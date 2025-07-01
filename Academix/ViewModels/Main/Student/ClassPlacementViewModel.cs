using Academix.Models;
using Academix.Services;
using Academix.Stores;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Windows;
using System.Windows.Input;
using Academix.DbContexts;

namespace Academix.ViewModels.Main.Student
{
    public class ClassPlacementViewModel : BaseViewModel
    {

        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;

        private bool _isAlreadyPlaced = false;
        public bool IsAlreadyPlaced
        {
            get => _isAlreadyPlaced;
            set
            {
                _isAlreadyPlaced = value;
                OnPropertyChanged(nameof(IsAlreadyPlaced));
            }
        }
        private ObservableCollection<StudentDisplayViewModel> _students;
        public ObservableCollection<StudentDisplayViewModel> Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        private List<Khoi> _grades = new List<Khoi>();
        private List<Lop> _classes = new List<Lop>();

        private ObservableCollection<Khoi> _gradeList = new ObservableCollection<Khoi>();
        public ObservableCollection<Khoi> GradeList
        {
            get => _gradeList;
            set
            {
                _gradeList = value;
                OnPropertyChanged(nameof(GradeList));
            }
        }
        private Khoi _selectedGrade;
        public Khoi SelectedGrade
        {
            get => _selectedGrade;
            set
            {
                _selectedGrade = value;
                SelectedClass = null;
                ClassList = new ObservableCollection<Lop>(_classes.Where(l => l.Makhoi == _selectedGrade.Makhoi));

                OnPropertyChanged(nameof(SelectedGrade));
            }
        }

        private ObservableCollection<Lop> _classlist = new ObservableCollection<Lop>();
        public ObservableCollection<Lop> ClassList
        {
            get => _classlist;
            set
            {
                _classlist = value;
                OnPropertyChanged(nameof(ClassList));
            }
        }

        private Lop _selectedClass;
        public Lop SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                OnPropertyChanged(nameof(SelectedClass));
            }
        }

        public IList SelectedStudents { get; set; } = new List<StudentDisplayViewModel>();
        public ICommand NotPlaceFilterCommand { get; }
        public ICommand PlaceStudentCommand { get; }
        public ICommand UnPlaceStudentCommand { get; }
       

        private ObservableCollection<TreeItemViewModel> _treeItems = new ObservableCollection<TreeItemViewModel>();
        public ObservableCollection<TreeItemViewModel> TreeItems
        {
            get => _treeItems;
            set
            {
                _treeItems = value;
                OnPropertyChanged(nameof(TreeItems));
            }
        }

        private TreeItemViewModel _selectedTreeItem;
        public TreeItemViewModel SelectedTreeItem
        {
            get => _selectedTreeItem;
            set
            {
                if (value != null && value.Item is Lop lop)
                {
                    Filter(lop);
                }
                _selectedTreeItem = value;
                OnPropertyChanged(nameof(SelectedTreeItem));
            }
        }

        private int _notPlaceNum;
        public int NotPlaceNum
        {
            get => _notPlaceNum;
            set
            {
                _notPlaceNum = value;
                OnPropertyChanged(nameof(NotPlaceNum));
            }
        }

        public ClassPlacementViewModel(NavigationService  navigationService, SchoolYearStore schoolYearStore)
        {
            
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            NotPlaceFilterCommand = new AsyncRelayCommand(NotPlaceFilter);
            PlaceStudentCommand = new AsyncRelayCommand(PlaceStudent);
            UnPlaceStudentCommand = new AsyncRelayCommand(UnPlaceStudent);
            _schoolYearStore.SelectedSchoolYearChanged += Update;
            Task.Run(LoadDataAsync).ConfigureAwait(false);
        }

        ~ClassPlacementViewModel()
        {
            _schoolYearStore.SelectedSchoolYearChanged -= Update;
        }

        private void Update()
        {
            LoadDataAsync();
        }
         
        private async Task LoadDataAsync()
        {
            try
            {
                using (var context = new QuanlyhocsinhContext())
                {
                    _grades = await context.Khois.ToListAsync();
                    _classes = await context.Lops.Where(l => l.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc).ToListAsync();
                    GradeList = new ObservableCollection<Khoi>(_grades);
                    ObservableCollection<TreeItemViewModel> treeItemViewModels = new ObservableCollection<TreeItemViewModel>();
                    foreach(Khoi grade in _grades)
                    {
                        TreeItemViewModel gradeTreeItem = new TreeItemViewModel(grade);
                        foreach(Lop @class in _classes.Where(l => l.Makhoi == grade.Makhoi).ToList())
                        {
                            gradeTreeItem.Children.Add(new TreeItemViewModel(@class));
                        }
                        treeItemViewModels.Add(gradeTreeItem);
                    }
                    TreeItems = treeItemViewModels;
                    Namhoc previousSchoolYear = await context.Namhocs.FirstOrDefaultAsync(nh => nh.Nam1 == _schoolYearStore.SelectedSchoolYear.Nam1 - 1);
                    List<Hocsinh> students;
                    if (previousSchoolYear != null)
                    {
                        students = await context.Hocsinhs
                                               .Include(hs => hs.CtLops.Where(ct => ct.MalopNavigation.Manamhoc == previousSchoolYear.Manamhoc))
                                               .ThenInclude(ct => ct.MalopNavigation)
                                               .Where(hs => !hs.CtLops.Any(ct => ct.MalopNavigation.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc) && !hs.CtLops.Any(ct => ct.MalopNavigation.Makhoi == "K12"))
                                               .ToListAsync();
                        //List<Hocsinh> students = await context.Hocsinhs
                        //    .Where(hs => hs.CtLops.Count == 0 || hs.CtLops.OrderBy(ct => ct.MalopNavigation.Manamhoc)
                        //                                                    .LastOrDefault().MalopNavigation.Manamhoc != _schoolYearStore.SchoolYears
                        //                                                                                                        .LastOrDefault().Manamhoc)
                        //    .Include(hs => hs.CtLops)
                        //    .ThenInclude(ct => ct.MalopNavigation)
                        //    .ToListAsync();
                        
                    }
                    else
                    {
                         students = await context.Hocsinhs                                             
                                              .Where(hs => !hs.CtLops.Any(ct => ct.MalopNavigation.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc) && !hs.CtLops.Any(ct => ct.MalopNavigation.Makhoi == "K12"))
                                              .ToListAsync();
                    }
                    ObservableCollection<StudentDisplayViewModel> studentDisplayViewModels = new ObservableCollection<StudentDisplayViewModel>();
                    foreach (Hocsinh student in students)
                    {
                        studentDisplayViewModels.Add(new StudentDisplayViewModel(student));
                    }
                    NotPlaceNum = studentDisplayViewModels.Count;
                    Students = studentDisplayViewModels;

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

        private async Task Filter(Lop @class)
        {
            using (var context = new QuanlyhocsinhContext())
            {
                Namhoc previousSchoolYear = await context.Namhocs.FirstOrDefaultAsync(nh => nh.Nam1 == _schoolYearStore.SelectedSchoolYear.Nam1 - 1);
                List<Hocsinh> students;
                if (previousSchoolYear != null)
                {
                    students = await context.Hocsinhs.Where(hs => hs.CtLops.Any(ct => ct.Malop == @class.Malop))
                   .Include(hs => hs.CtLops.Where(ct => ct.MalopNavigation.Manamhoc == previousSchoolYear.Manamhoc))
                   .ThenInclude(ct => ct.MalopNavigation)
                   .ToListAsync();
                }
                else
                {
                    students = await context.Hocsinhs.Where(hs => hs.CtLops.Any(ct => ct.Malop == @class.Malop))
                  .ToListAsync();
                }

                    ObservableCollection<StudentDisplayViewModel> studentDisplayViewModels = new ObservableCollection<StudentDisplayViewModel>();
                foreach (Hocsinh student in students)
                {
                    studentDisplayViewModels.Add(new StudentDisplayViewModel(student));
                }
                Students = studentDisplayViewModels;
            }
            IsAlreadyPlaced = true;

        }

        
        private async Task NotPlaceFilter()
        {
            try
            {
                using (var context = new QuanlyhocsinhContext())
                {
                    Namhoc previousSchoolYear = await context.Namhocs.FirstOrDefaultAsync(nh => nh.Nam1 == _schoolYearStore.SelectedSchoolYear.Nam1 - 1);
                    List<Hocsinh> students;
                    if (previousSchoolYear != null)
                    {
                        students = await context.Hocsinhs
                                               .Include(hs => hs.CtLops.Where(ct => ct.MalopNavigation.Manamhoc == previousSchoolYear.Manamhoc))
                                               .ThenInclude(ct => ct.MalopNavigation)
                                               .Where(hs => !hs.CtLops.Any(ct => ct.MalopNavigation.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc) && !hs.CtLops.Any(ct => ct.MalopNavigation.Makhoi == "K12"))
                                               .ToListAsync();
                        //List<Hocsinh> students = await context.Hocsinhs
                        //    .Where(hs => hs.CtLops.Count == 0 || hs.CtLops.OrderBy(ct => ct.MalopNavigation.Manamhoc)
                        //                                                    .LastOrDefault().MalopNavigation.Manamhoc != _schoolYearStore.SchoolYears
                        //                                                                                                        .LastOrDefault().Manamhoc)
                        //    .Include(hs => hs.CtLops)
                        //    .ThenInclude(ct => ct.MalopNavigation)
                        //    .ToListAsync();
                       
                    }
                    else
                    {
                        students = await context.Hocsinhs
                                              .Where(hs => !hs.CtLops.Any(ct => ct.MalopNavigation.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc) && !hs.CtLops.Any(ct => ct.MalopNavigation.Makhoi == "K12"))
                                              .ToListAsync();
                    }
                    ObservableCollection<StudentDisplayViewModel> studentDisplayViewModels = new ObservableCollection<StudentDisplayViewModel>();
                    foreach (Hocsinh student in students)
                    {
                        studentDisplayViewModels.Add(new StudentDisplayViewModel(student));
                    }
                    Students = studentDisplayViewModels;
                    SelectedTreeItem = null;
                    IsAlreadyPlaced = false;

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

        private async Task UnPlaceStudent()
        {
            try
            {
                
                if (SelectedStudents.Count == 0)
                    throw new Exception("Xin hãy chọn học sinh muốn hủy phân lớp!");

                using (var context = new QuanlyhocsinhContext())
                {


                  
                    List<StudentDisplayViewModel> studentDisplayViewModels = SelectedStudents.Cast<StudentDisplayViewModel>().ToList();

                    foreach (StudentDisplayViewModel student in studentDisplayViewModels)
                    {

                        List<CtLop> ctLops = await context.CtLops.Where(ct => ct.Mahs == student.Id && ct.MalopNavigation.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc).ToListAsync();
                        foreach (CtLop ctLop in ctLops)
                        {
                            context.CtLops.Remove(ctLop);
                        }


                        await context.SaveChangesAsync();
                        foreach (TreeItemViewModel treeItemViewModel in _treeItems)
                        {
                            if (treeItemViewModel.Id == ((Lop)_selectedTreeItem.Item).Makhoi)
                            {
                                foreach (TreeItemViewModel child in treeItemViewModel.Children)
                                {
                                    if (child.Id == _selectedTreeItem.Id && child.Item is Lop @class)
                                    {
                                        
                                       @class.Siso -= SelectedStudents.Count;
                                       SelectedTreeItem.NotifyItemChange();
                                        NotPlaceNum += SelectedStudents.Count;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        foreach (StudentDisplayViewModel studentDisplayViewModel in studentDisplayViewModels)
                        {
                            Students.Remove(studentDisplayViewModel);
                        }
                        SelectedStudents.Clear();


                    }
                    MessageBox.Show("Hủy phân lớp thành công!");


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
        private async Task PlaceStudent()
        {
            try
            {
                if (_selectedClass == null)
                    throw new Exception("Xin hãy chọn lớp muốn chuyển!");
                if(SelectedStudents.Count == 0)
                    throw new Exception("Xin hãy chọn học sinh muốn chuyển!");

                using (var context = new QuanlyhocsinhContext())
                {
                    Thamso MaximumClassSize = await context.Thamsos.FirstOrDefaultAsync(ts => ts.Tenthamso == "SiSoToiDa");

                    if (_isAlreadyPlaced && _selectedClass.Malop == _selectedTreeItem.Id)
                        throw new Exception("Học sinh đã có trong lớp!");
                        List<StudentDisplayViewModel> studentDisplayViewModels = SelectedStudents.Cast<StudentDisplayViewModel>().ToList();
                     
                        if(MaximumClassSize != null && (_selectedClass.Siso + SelectedStudents.Count) > MaximumClassSize.Giatri)
                        
                            throw new Exception($"Sĩ số vượt mức tối đa là {MaximumClassSize.Giatri}!");

                    

                    foreach (StudentDisplayViewModel student in studentDisplayViewModels)
                        {

                            List<CtLop> ctLops = await context.CtLops.Where(ct => ct.Mahs == student.Id && ct.MalopNavigation.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc).ToListAsync();
                            foreach (CtLop ctLop in ctLops)
                            {
                                context.CtLops.Remove(ctLop);
                            }


                            List<Hocky> semesters = await context.Hockies.ToListAsync();
                            foreach (Hocky semester in semesters)
                            {
                                context.CtLops.Add(new CtLop(_selectedClass.Malop, student.Id, semester.Mahocky, 0));

                            }

                            
                        
                    }
                    await context.SaveChangesAsync();
                    foreach (TreeItemViewModel treeItemViewModel in _treeItems)
                    {
                        if (treeItemViewModel.Id == _selectedGrade.Makhoi)
                        {
                            foreach (TreeItemViewModel child in treeItemViewModel.Children)
                            {
                                if (child.Id == _selectedClass.Malop && child.Item is Lop clas)
                                {
                                    if (_isAlreadyPlaced && _selectedTreeItem != null && SelectedTreeItem.Item is Lop @class)
                                    {
                                        
                                        @class.Siso -= SelectedStudents.Count;
                                        SelectedTreeItem.NotifyItemChange();
                                    }
                                    clas.Siso += SelectedStudents.Count;
                                    child.NotifyItemChange();
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    foreach (StudentDisplayViewModel studentDisplayViewModel in studentDisplayViewModels)
                    {
                        Students.Remove(studentDisplayViewModel);
                    }

                    if (!IsAlreadyPlaced)
                        NotPlaceNum -= studentDisplayViewModels.Count;

                    SelectedStudents.Clear();
                    MessageBox.Show("Chuyển lớp thành công!");


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


        
       

    }
}
