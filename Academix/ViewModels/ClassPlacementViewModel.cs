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


namespace Academix.ViewModels
{
    public class ClassPlacementViewModel : BaseViewModel
    {

        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;
      

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
                OnPropertyChanged(nameof(SelectedGrade));
            }
        }

        public IList SelectedStudents { get; set; } = new List<StudentDisplayViewModel>();
        public ICommand NotPlaceFilterCommand { get; }
        public ICommand PlaceStudentCommand { get; }

        private List<Khoi> _grades = new List<Khoi>();
        private List<Lop> _classes = new List<Lop>();

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
                //if(value.Item is Lop lop)
                //{
                //    Filter(lop);
                //}
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
            Task.Run(LoadDataAsync).ConfigureAwait(false);
        }
         
        private async Task LoadDataAsync()
        {
            try
            {
                using (var context = new QuanlyhocsinhContext())
                {
                    _grades = await context.Khois.Where(k => k.Makhoi == "K10").ToListAsync();
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
                    List<Hocsinh> students = await context.Hocsinhs
                        .Where(hs => hs.CtLops.Count == 0)
                        .Include(hs => hs.CtLops)
                        .ThenInclude(ct => ct.MalopNavigation)
                        .ToListAsync();
                    //List<Hocsinh> students = await context.Hocsinhs
                    //    .Where(hs => hs.CtLops.Count == 0 || hs.CtLops.OrderBy(ct => ct.MalopNavigation.Manamhoc)
                    //                                                    .LastOrDefault().MalopNavigation.Manamhoc != _schoolYearStore.SchoolYears
                    //                                                                                                        .LastOrDefault().Manamhoc)
                    //    .Include(hs => hs.CtLops)
                    //    .ThenInclude(ct => ct.MalopNavigation)
                    //    .ToListAsync();
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

        //private async Task Filter(Lop @class)
        //{
        //    using (var context = new QuanlyhocsinhContext())
        //    {
        //        List<Hocsinh> students = await context.Hocsinhs.Where(hs => hs.CtLops.Any(ct => ct.Malop == @class.Malop)).ToListAsync();
        //        ObservableCollection<StudentDisplayViewModel> studentDisplayViewModels = new ObservableCollection<StudentDisplayViewModel>();
        //        foreach(Hocsinh student in students)
        //        {
        //            studentDisplayViewModels.Add(new StudentDisplayViewModel(student));
        //        }
        //        Students = studentDisplayViewModels;
        //    }
        //}

        private async Task Filter()
        {
            if (_selectedGrade == null)
                return;
            string underGradeId = "";

            switch (_selectedGrade.Makhoi)
            {
                case "K10":
                    break;
                case "K11":
                    underGradeId = "K10";
                    break;
                case "K12":
                    underGradeId = "K11";
                    break;

            }

            using (var context = new QuanlyhocsinhContext())
            {
                if(string.IsNullOrEmpty(underGradeId))
                {

                }
                else
                {
                    List<Hocsinh> students = await context.Hocsinhs.Where(hs => hs.CtLops.Any(ct => ct.MalopNavigation.Makhoi == underGradeId)).ToListAsync();

                }

                //ObservableCollection<StudentDisplayViewModel> studentDisplayViewModels = new ObservableCollection<StudentDisplayViewModel>();
                //foreach (Hocsinh student in students)
                //{
                //    studentDisplayViewModels.Add(new StudentDisplayViewModel(student));
                //}
                //Students = studentDisplayViewModels;
            }
        }
        private async Task NotPlaceFilter()
        {
            using (var context = new QuanlyhocsinhContext())
            {
                
                List<Hocsinh> students = await context.Hocsinhs
                    .Where(hs => hs.CtLops.Count == 0)
                    .Include(hs => hs.CtLops)
                    .ThenInclude(ct => ct.MalopNavigation)
                    .ToListAsync();
                ObservableCollection<StudentDisplayViewModel> studentDisplayViewModels = new ObservableCollection<StudentDisplayViewModel>();
                foreach (Hocsinh student in students)
                {
                    studentDisplayViewModels.Add(new StudentDisplayViewModel(student));
                }
                NotPlaceNum = studentDisplayViewModels.Count;

                Students = studentDisplayViewModels;
            }
        }

        private async Task PlaceStudent()
        {
            try
            {
                if (_selectedTreeItem == null || _selectedTreeItem.Item is not Lop @class)
                    throw new Exception("Xin hãy chọn lớp muốn chuyển!");
                if(SelectedStudents.Count == 0)
                    throw new Exception("Xin hãy chọn học sinh muốn chuyển!");

                using (var context = new QuanlyhocsinhContext())
                {
                    List<StudentDisplayViewModel> studentDisplayViewModels = SelectedStudents.Cast<StudentDisplayViewModel>().ToList();
                    foreach(StudentDisplayViewModel student in studentDisplayViewModels)
                    {
                        //CtLop ctLop = await context.CtLops.FirstOrDefaultAsync(ct => ct.MalopNavigation.Manamhoc == _schoolYearStore.SchoolYears.LastOrDefault().Manamhoc);
                        //if (ctLop != null)
                        //{
                        //    context.CtLops.Remove(ctLop);
                        //    CtLop ct = new CtLop(@class.Malop, student.Id, "HK1", 0);
                        //    context.CtLops.Add(ct);
                        //}
                        //else
                        //{
                            CtLop ct = new CtLop(@class.Malop, student.Id, "HK1", 0);
                            context.CtLops.Add(ct);

                        //}
                
                    }
                    await context.SaveChangesAsync();
                    @class.Siso += studentDisplayViewModels.Count;
                    ObservableCollection<TreeItemViewModel> treeItemViewModels = new ObservableCollection<TreeItemViewModel>();
                    foreach (Khoi grade in _grades)
                    {
                        TreeItemViewModel gradeTreeItem = new TreeItemViewModel(grade);
                        foreach (Lop @classs in _classes.Where(l => l.Makhoi == grade.Makhoi).ToList())
                        {
                            gradeTreeItem.Children.Add(new TreeItemViewModel(@classs));
                        }
                        treeItemViewModels.Add(gradeTreeItem);
                    }
                    TreeItems = treeItemViewModels;

                }
                await NotPlaceFilter();
                MessageBox.Show("Chuyển lớp thành công!");

            }
            catch
            {

            }
            finally
            {

            }
           
        }


        //public ObservableCollection<GradeGroup> Grades { get; set; } = new();
        //public ObservableCollection<ClassGroup> Classes { get; set; } = new();
        //public IRelayCommand<ClassGroup> SelectClassCommand { get;  }
        //public ClassPlacementViewModel()
        //{

        //    Students = new ObservableCollection<StudentDisplayModel>();
        //    Grades = new ObservableCollection<GradeGroup>();
        //    Classes = new ObservableCollection<ClassGroup>();

        //    Students.CollectionChanged += (s, e) => UpdateIndexes();

        //    Students = new ObservableCollection<StudentDisplayModel>
        //    {
        //        new StudentDisplayModel(
        //            new Student("HS001", "Nguyễn Văn A", true, new DateTime(2007, 5, 10), "123 Đường A", "a@example.com", null),
        //            "10/1", 8.2, 8.5, "Đang học", 1, false),

        //        new StudentDisplayModel(
        //            new Student("HS002", "Trần Thị B", false, new DateTime(2007, 8, 15), "456 Đường B", "b@example.com", null),
        //            "10/1", 7.5, 7.8, "Đang học", 2, false),

        //        new StudentDisplayModel(
        //            new Student("HS003", "Lê Văn C", true, new DateTime(2007, 2, 20), "789 Đường C", "c@example.com", null),
        //            "10/2", 6.0, 6.5, "Nghỉ học", 3, false),

        //        new StudentDisplayModel(
        //            new Student("HS004", "Phạm Thị D", false, new DateTime(2006, 11, 5), "101 Đường D", "d@example.com", null),
        //            "11/1", 9.1, 9.4, "Đang học", 4, false),

        //        new StudentDisplayModel(
        //            new Student("HS005", "Đặng Văn E", true, new DateTime(2006, 7, 25), "202 Đường E", "e@example.com", null),
        //            "11/2", 5.0, 4.8, "Nghỉ học", 5, false),

        //        new StudentDisplayModel(
        //            new Student("HS006", "Hoàng Thị F", false, new DateTime(2006, 3, 12), "303 Đường F", "f@example.com", null),
        //            "11/3", 6.2, 6.9, "Đang học", 6, false),

        //        new StudentDisplayModel(
        //            new Student("HS007", "Mai Văn G", true, new DateTime(2005, 10, 30), "404 Đường G", "g@example.com", null),
        //            "12/1", 8.0, 8.3, "Đang học", 7, false),

        //        new StudentDisplayModel(
        //            new Student("HS008", "Ngô Thị H", false, new DateTime(2005, 4, 18), "505 Đường H", "h@example.com", null),
        //            "12/1", 9.0, 9.5, "Đang học", 8, false),

        //        new StudentDisplayModel(
        //            new Student("HS009", "Bùi Văn I", true, new DateTime(2005, 6, 8), "606 Đường I", "i@example.com", null),
        //            "12/2", 7.2, 7.1, "Nghỉ học", 9, false),

        //        new StudentDisplayModel(
        //            new Student("HS010", "Đỗ Thị K", false, new DateTime(2005, 9, 22), "707 Đường K", "k@example.com", null),
        //            "12/3", 8.5, 8.6, "Đang học", 10, false),

        //        new StudentDisplayModel(
        //            new Student("HS011", "Trịnh Văn L", true, new DateTime(2007, 1, 14), "808 Đường L", "l@example.com", null),
        //            "10/1", 5.5, 5.8, "Đang học", 11, false),

        //        new StudentDisplayModel(
        //            new Student("HS012", "Phan Thị M", false, new DateTime(2006, 2, 27), "909 Đường M", "m@example.com", null),
        //            "11/1", 7.8, 7.7, "Đang học", 12, false),

        //        new StudentDisplayModel(
        //            new Student("HS013", "Vũ Văn N", true, new DateTime(2006, 5, 3), "111 Đường N", "n@example.com", null),
        //            "11/2", 6.3, 6.2, "Nghỉ học", 13, false),

        //        new StudentDisplayModel(
        //            new Student("HS014", "Lý Thị O", false, new DateTime(2007, 12, 19), "222 Đường O", "o@example.com", null),
        //            "10/2", 9.0, 8.9, "Đang học", 14, false),

        //        new StudentDisplayModel(
        //            new Student("HS015", "Nguyễn Văn P", true, new DateTime(2005, 8, 7), "333 Đường P", "p@example.com", null),
        //            "12/3", 7.0, 7.4, "Đang học", 15, false)
        //    };

        //    // Bước 2: Nhóm học sinh theo lớp
        //    var ClassGroups = Students
        //        .GroupBy(s => s.ClassName)
        //        .Select(g => new ClassGroup
        //        {
        //            ClassName = g.Key,
        //            Students = new ObservableCollection<StudentDisplayModel>(g)
        //        })
        //        .ToList();

        //    // Bước 3: Nhóm các lớp theo khối (dựa trên phần đầu của tên lớp)
        //    var GradeGroups = ClassGroups
        //        .GroupBy(cg => $"Khối {cg.ClassName.Split('/')[0]}")
        //        .Select(g => new GradeGroup
        //        {
        //            GradeName = g.Key,
        //            Classes = new ObservableCollection<ClassGroup>(g)
        //        });

        //    // Bước 4: Gán vào ViewModel
        //    Grades = new ObservableCollection<GradeGroup>(GradeGroups);
        //    SelectClassCommand = new RelayCommand<ClassGroup>(ClassGroup =>
        //    {
        //        SelectedClass = ClassGroup;
        //    });


        //    foreach (var student in Students)
        //    {
        //        student.PropertyChanged += Student_PropertyChanged;
        //    }

        //    UpdateIndexes();
        //}

        //private void UpdateIndexes()
        //{
        //    for (int i = 0; i < Students.Count; i++)
        //    {
        //        Students[i].Index = i + 1;
        //    }
        //}

        //private void Student_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(StudentDisplayModel.IsSelected))
        //    {
        //        UpdateIsAllSelected();
        //    }
        //}

        //private bool? _isAllSelected = false;
        //private bool _isUpdatingFromStudents = false;

        //public bool? IsAllSelected
        //{
        //    get => _isAllSelected;
        //    set
        //    {
        //        if (_isAllSelected == value)
        //            return;

        //        _isAllSelected = value;
        //        OnPropertyChanged(nameof(IsAllSelected));

        //        if (value.HasValue && !_isUpdatingFromStudents)
        //        {
        //            foreach (var student in Students)
        //            {
        //                student.IsSelected = value.Value;
        //            }
        //        }
        //    }
        //}

        //private void UpdateIsAllSelected()
        //{
        //    _isUpdatingFromStudents = true;

        //    var selectedCount = Students.Count(s => s.IsSelected);
        //    if (selectedCount == Students.Count)
        //    {
        //        IsAllSelected = true;
        //    }
        //    else if (selectedCount == 0)
        //    {
        //        IsAllSelected = false;
        //    }
        //    else
        //    {
        //        IsAllSelected = false;
        //    }

        //    _isUpdatingFromStudents = false;
        //}

        //private ClassGroup _selectedClass;
        //public ClassGroup SelectedClass
        //{
        //    get => _selectedClass;
        //    set
        //    {
        //        if (_selectedClass != value)
        //        {
        //            _selectedClass = value;
        //            OnPropertyChanged(nameof(SelectedClass));
        //            Students = _selectedClass?.Students ?? new ObservableCollection<StudentDisplayModel>();
        //            OnPropertyChanged(nameof(Students));
        //            UpdateIndexes();
        //        }
        //    }
        //}

       

    }
}
