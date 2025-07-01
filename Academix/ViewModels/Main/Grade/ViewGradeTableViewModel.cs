using Academix.Models;
using Academix.Services;
using Academix.Stores;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Academix.DbContexts;
using System.Windows.Controls;
using System.Windows.Data;


namespace Academix.ViewModels.Main.Grade
{
    class ViewGradeTableViewModel:BaseViewModel
    {
        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;
        private Bangdiemmonhoc _bangdiemmonhoc;
      
        private ObservableCollection<GradeTableItemViewModel> _gradeTableItems;
        public ObservableCollection<GradeTableItemViewModel> GradeTableItems
        {
            get => _gradeTableItems;
            set
            {
                _gradeTableItems = value;
                OnPropertyChanged(nameof(GradeTableItems));
            }
        }

        private ObservableCollection<DataGridColumn> _dynamicColumns = new ObservableCollection<DataGridColumn>();
        public ObservableCollection<DataGridColumn> DynamicColumns
        {
            get => _dynamicColumns;
            set
            {
                _dynamicColumns = value;
                OnPropertyChanged(nameof(DynamicColumns));
            }
        }

        public string ClassName => _bangdiemmonhoc.MalopNavigation.Tenlop;
        public string SubjectName => _bangdiemmonhoc.MamhNavigation.Tenmh;
        public string SemesterName => _bangdiemmonhoc.MahockyNavigation.Tenhocky;
        public string SchoolYearName => _schoolYearStore.SelectedSchoolYear.ToString();
        

        public ICommand BackCommand { get; }

        public ViewGradeTableViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore, Bangdiemmonhoc bangdiemmonhoc)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            _bangdiemmonhoc = bangdiemmonhoc;
            BackCommand = new RelayCommand(Back);
            Task.Run(LoadDataAsync).ConfigureAwait(false);
        }

        private async Task LoadDataAsync()
        {
            using (var context = new QuanlyhocsinhContext())
            {
                List<Hocsinh> students = await context.Hocsinhs
                                                      .Where(hs => hs.CtLops.Any(ct => ct.Malop == _bangdiemmonhoc.Malop))
                                                      .Include(hs => hs.CtBangdiemmonhocs.Where(ct => ct.Mabdmh == _bangdiemmonhoc.Mabdmh))
                                                      .ThenInclude(ct => ct.CtDiemmonhocs)
                                                      .ToListAsync();
                List<Loaidiem> scoreTypes = await context.Loaidiems.OrderBy(ld => ld.Hesold).ToListAsync();
                ObservableCollection<GradeTableItemViewModel> gradeTableItemViewModels = new ObservableCollection<GradeTableItemViewModel>();
                foreach(Hocsinh student in students)
                {
                    GradeTableItemViewModel gradeTableItemViewModel = new GradeTableItemViewModel(student);
                    if(student.CtBangdiemmonhocs.Count > 0)
                    {
                        //foreach (Loaidiem scoreType in scoreTypes)
                        //{
                        //    foreach (CtDiemmonhoc ctDiemmonhoc in student.CtBangdiemmonhocs.FirstOrDefault().CtDiemmonhocs.Where(ct => ct.Maloaidiem == scoreType.Maloaidiem))
                        //    {
                        //        if (gradeTableItemViewModel.Scores[scoreType.Maloaidiem] == null)
                        //            gradeTableItemViewModel.Scores[scoreType.Maloaidiem] = "";
                        //        gradeTableItemViewModel.Scores[scoreType.Maloaidiem] += ctDiemmonhoc.Diem + " ";


                        //    }
                        //}
                        foreach (CtDiemmonhoc ctDiemmonhoc in student.CtBangdiemmonhocs.FirstOrDefault().CtDiemmonhocs.Where(ct => ct.Maloaidiem == "M1"))
                        {
                            gradeTableItemViewModel.OralScoresList.Add(ctDiemmonhoc.Diem);

                        }

                        foreach (CtDiemmonhoc ctDiemmonhoc in student.CtBangdiemmonhocs.FirstOrDefault().CtDiemmonhocs.Where(ct => ct.Maloaidiem == "M2"))
                        {
                            gradeTableItemViewModel.ShortScoresList.Add(ctDiemmonhoc.Diem);

                        }
                        foreach (CtDiemmonhoc ctDiemmonhoc in student.CtBangdiemmonhocs.FirstOrDefault().CtDiemmonhocs.Where(ct => ct.Maloaidiem == "M3"))
                        {
                            gradeTableItemViewModel.PeriodScoresList.Add(ctDiemmonhoc.Diem);

                        }
                        foreach (CtDiemmonhoc ctDiemmonhoc in student.CtBangdiemmonhocs.FirstOrDefault().CtDiemmonhocs.Where(ct => ct.Maloaidiem == "M4"))
                        {
                            gradeTableItemViewModel.FinalScoresList.Add(ctDiemmonhoc.Diem);

                        }
                        gradeTableItemViewModel.GPA = Math.Round(student.CtBangdiemmonhocs.FirstOrDefault().Dtbmon, 2);
                    }
                    
                   
                    gradeTableItemViewModels.Add(gradeTableItemViewModel);
                }
                //foreach (Loaidiem scoretype in scoreTypes)
                //{
                //    DynamicColumns.Add(new DataGridTextColumn()
                //    {
                //        Header = scoretype.Tenloaidiem,
                //        Width = 100,
                //        //Binding = new Binding($"gradeTableItemViewModel.Scores[{scoretype.Maloaidiem}]")

                //    });

                //}
                GradeTableItems = gradeTableItemViewModels;
            }
        }

        private void Back()
        {
            BaseViewModel viewModel = _navigationService.PopStack();
            if(viewModel != null)
            {
                _navigationService.Navigate(viewModel);
            }
        }

        public override string ToString()
        {
            return "Lớp >> Xem bảng điểm môn";
        }

    }
}
