using Academix.Models;
using Academix.Models.Main;
using Academix.Services;
using Academix.Stores;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Academix.DbContexts;


namespace Academix.ViewModels.Main.Report
{
    public class SubjectReportViewModel : BaseViewModel
    {
        private ObservableCollection<Hocky> _semesters;
        private ObservableCollection<Monhoc> _subjects;
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
        public ObservableCollection<Monhoc> Subjects
        {
            get
            {
                return _subjects;
            }
            set
            {
                _subjects = value;
                OnPropertyChanged(nameof(Subjects));
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

        private Monhoc _selectedSubject;
        public Monhoc SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                if (_selectedSubject != value)
                {
                    _selectedSubject = value;
                    OnPropertyChanged(nameof(SelectedSubject));
                }
            }
        }

        private SchoolYearStore _schoolYearStore;


        public string SelectedSubjectName
        {
            get => _selectedSubject.Tenmh;
            set
            {
                _selectedSubject.Tenmh = value;
                OnPropertyChanged(nameof(SelectedSubjectName));
            }
        }

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
                if(!_schoolYearStore.SelectedSchoolYear.IsAll)
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
                List<Monhoc> subjects = await context.Monhocs.ToListAsync();
                Semesters = new ObservableCollection<Hocky>(semesters);
                Subjects = new ObservableCollection<Monhoc>(subjects);



            }
        }

        private ObservableCollection<SubjectReportItemViewModel> _reportItems;
        public ObservableCollection<SubjectReportItemViewModel> ReportItems
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
        public SubjectReportViewModel(SchoolYearStore schoolYearStore)
        {
            Task.Run(LoadDataAsync).ConfigureAwait(false);
            _schoolYearStore = schoolYearStore;
            _reportItems = new ObservableCollection<SubjectReportItemViewModel>();
            ExportReportCommand = new AsyncRelayCommand(CreateReport);
        }





        
        private async Task FilterData()
        {
            try
            {


                using(var context = new QuanlyhocsinhContext())
                {
                     Thamso subjectPassingGrade = await context.Thamsos.FirstOrDefaultAsync(ts => ts.Tenthamso == "DiemDatMon");

                    Bctongketmon bctongketmon = await context.Bctongketmons
                        .Include(bd => bd.CtBctongketmons)
                        .ThenInclude(ct => ct.MalopNavigation)
                        .FirstOrDefaultAsync(bc => bc.Mamh == _selectedSubject.Mamh && bc.Mahocky == _selectedSemester.Mahocky && bc.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc);
                    List<SubjectReportItemViewModel> subjectReportItemViewModels = new List<SubjectReportItemViewModel>();

                    if (bctongketmon != null)
                    {
                        foreach(CtBctongketmon ctBctongketmon in bctongketmon.CtBctongketmons)
                        {
                            SubjectReportItemViewModel subjectReportItemViewModel = new SubjectReportItemViewModel(ctBctongketmon.MalopNavigation, ctBctongketmon.Soluongdat,Math.Round(ctBctongketmon.Tiledat, 2));
                            subjectReportItemViewModels.Add(subjectReportItemViewModel);
                        }
                    }
                    else
                    {
                        List<Lop> classes = await context.Lops
                                                                               .Where(l => l.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc)
                                                                               .Include(l => l.CtLops)
                                                                               .Include(l => l.Bangdiemmonhocs.Where(bd => bd.Mamh == _selectedSubject.Mamh & bd.Mahocky == _selectedSemester.Mahocky))
                                                                               .ThenInclude(bd => bd.CtBangdiemmonhocs.Where(ct => ct.Dtbmon >= subjectPassingGrade.Giatri))
                                                                               .ToListAsync();

                        foreach (Lop classr in classes)
                        {
                            int count = 0;
                            double passingRate = 0;
                            Bangdiemmonhoc bangdiemmonhoc = classr.Bangdiemmonhocs.FirstOrDefault();
                            if (bangdiemmonhoc != null && classr.Siso > 0)
                            {
                                count = bangdiemmonhoc.CtBangdiemmonhocs.Count;
                                passingRate = Math.Round(1d * count / classr.Siso, 2);
                                SubjectReportItemViewModel subjectReportItemViewModel = new SubjectReportItemViewModel(classr, count, passingRate);
                                subjectReportItemViewModels.Add(subjectReportItemViewModel);

                            }
                            else
                            {
                                SubjectReportItemViewModel subjectReportItemViewModel = new SubjectReportItemViewModel(classr, 0, 0);
                                subjectReportItemViewModels.Add(subjectReportItemViewModel);
                            }
                                
                        }


                        Bctongketmon subjectReport = new Bctongketmon(GenerateIdService.GenerateId(), _selectedSubject.Mamh, _selectedSemester.Mahocky, _schoolYearStore.SelectedSchoolYear.Manamhoc);
                        foreach (SubjectReportItemViewModel subjectReportItemVM in subjectReportItemViewModels)
                        {
                            CtBctongketmon ctBctongketmon = new CtBctongketmon(subjectReport.Mabctkmon, subjectReportItemVM.ClassId, subjectReportItemVM.ClassSize, subjectReportItemVM.Count, subjectReportItemVM.PassingRate);
                            subjectReport.CtBctongketmons.Add(ctBctongketmon);
                        }
                        await context.Bctongketmons.AddAsync(subjectReport);
                        await context.SaveChangesAsync();

                    }
                    ReportItems = new ObservableCollection<SubjectReportItemViewModel>(subjectReportItemViewModels);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }


           
        }


        private async Task CreateReport()
        {

            if (_schoolYearStore.SelectedSchoolYear == null || _schoolYearStore.SelectedSchoolYear.IsAll || _selectedSemester == null)
            {
                MessageBox.Show("Vui lòng chọn thông tin năm học, học  kỳ và môn học!");
                return;

            }

            SelectedSubjectName = _selectedSubject.Tenmh;
            SelectedSemesterName = _selectedSemester.Tenhocky;
            SelectedSchoolYearName = _schoolYearStore.SelectedSchoolYear.ToString();
            await FilterData();
        }
    }

   
}
