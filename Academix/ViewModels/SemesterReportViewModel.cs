using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Academix.Models;
using Academix.Services;
using Academix.Stores;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Academix.ViewModels
{
    public class SemesterReportViewModel : BaseViewModel
    {
        private ObservableCollection<Hocky> _semesters;
        public ObservableCollection<Hocky> Semesters
        {
            get
            {
                return _semesters;
            }
            set
            {
                _semesters = value;
                OnPropertyChanged(nameof(Semesters));
            }
        }
       

        public ICommand ExportReportCommand { get; }
       

        private Hocky _selectedSemester;
        public Hocky SelectedSemester
        {
            get => _selectedSemester;
            set
            {
                if (_selectedSemester != value)
                {
                    _selectedSemester = value;
                    OnPropertyChanged(nameof(SelectedSemester));
                }
            }
        }


        private SchoolYearStore _schoolYearStore;


        public string SelectedSemesterName
        {
            get => _selectedSemester.Tenhocky;
            set
            {
                _selectedSemester.Tenhocky = value;
                OnPropertyChanged(nameof(SelectedSemesterName));
            }
        }

        public string SelectedSchoolYearName
        {
            get
            {
                if (!_schoolYearStore.SelectedSchoolYear.IsAll)
                    return _schoolYearStore.SelectedSchoolYear.ToString();
                return "";
            }

            set
            {
                OnPropertyChanged(nameof(SelectedSchoolYearName));
            }
        }

        private async Task LoadDataAsync()
        {
            using (var context = new QuanlyhocsinhContext())
            {
                List<Hocky> semesters = await context.Hockies.ToListAsync();
                Semesters = new ObservableCollection<Hocky>(semesters);
            }
        }

        private ObservableCollection<SemesterReportItemViewModel> _reportItems;
        public ObservableCollection<SemesterReportItemViewModel> ReportItems
        {
            get
            {
                return _reportItems;
            }
            set
            {
                _reportItems = value;
                OnPropertyChanged(nameof(ReportItems));
            }
        }
        public SemesterReportViewModel(SchoolYearStore schoolYearStore)
        {
            Task.Run(LoadDataAsync).ConfigureAwait(false);
            _schoolYearStore = schoolYearStore;
            _reportItems = new ObservableCollection<SemesterReportItemViewModel>();
            ExportReportCommand = new AsyncRelayCommand(CreateReport);
        }


        private async Task FilterData()
        {
            try
            {


                using (var context = new QuanlyhocsinhContext())
                {
                    Thamso passingGrade = await context.Thamsos.FirstOrDefaultAsync(ts => ts.Tenthamso == "DiemDat");

                    List<Lop> classes = await context.Lops
                                                    .Where(l => l.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc)
                                                    .Include(l => l.CtLops.Where(ct => ct.Mahocky == _selectedSemester.Mahocky && ct.Dtbhk >= passingGrade.Giatri))
                                                    .ToListAsync();

                    List<SemesterReportItemViewModel> semesterReportItemViewModels = new List<SemesterReportItemViewModel>();
                    foreach (Lop classr in classes)
                    {
                        int count = 0;
                        float passingRate = 0;
                        if (!classr.CtLops.IsNullOrEmpty() && classr.Siso > 0)
                        {
                            count = classr.CtLops.Count;
                            passingRate = Convert.ToSingle(Math.Round(1f * count / classr.Siso, 2));

                        }
                        SemesterReportItemViewModel semesterReportItemViewModel = new SemesterReportItemViewModel(classr, count, passingRate);
                        semesterReportItemViewModels.Add(semesterReportItemViewModel);
                    }

                    ReportItems = new ObservableCollection<SemesterReportItemViewModel>(semesterReportItemViewModels);

                    Bctongkethocky semesterReport = new Bctongkethocky(GenerateIdService.GenerateId(), _selectedSemester.Mahocky, _schoolYearStore.SelectedSchoolYear.Manamhoc);
                    foreach (SemesterReportItemViewModel semesterReportItemVM in semesterReportItemViewModels)
                    {
                        CtBctongkethocky ctBctongkethocky = new CtBctongkethocky(GenerateIdService.GenerateId(), semesterReportItemVM.ClassId, semesterReportItemVM.ClassSize, semesterReportItemVM.Count, Convert.ToDouble(semesterReportItemVM.PassingRate));
                        semesterReport.CtBctongkethockies.Add(ctBctongkethocky);
                    }
                    await context.Bctongkethockies.AddAsync(semesterReport);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }



        }


        private async Task CreateReport()
        {

            if (_schoolYearStore.SelectedSchoolYear == null || _schoolYearStore.SelectedSchoolYear.IsAll || _selectedSemester == null )
            {
                MessageBox.Show("Vui lòng chọn thông tin năm học và học  kỳ!");
                return;

            }

            SelectedSemesterName = _selectedSemester.Tenhocky;
            SelectedSchoolYearName = _schoolYearStore.SelectedSchoolYear.ToString();
            await FilterData();
        }
    }


}

    

