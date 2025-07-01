using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Academix.Models;
using System.ComponentModel;
using ControlzEx.Standard;
using Academix.Views;
using Academix.Stores;
using Academix.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Academix.DbContexts;
using System.Runtime.InteropServices.JavaScript;

namespace Academix.ViewModels.Main.Student
{
    public class SearchStudentViewModel : BaseViewModel
    {
        private Khoi _selectedGrade;
        private ObservableCollection<Khoi> _gradeList;
        public ObservableCollection<Khoi> GradeList 
        { get => _gradeList; 
          set
            {
                _gradeList = value;
                OnPropertyChanged(nameof(GradeList));
            } 
        }


        public Khoi SelectedGrade
        {
            get => _selectedGrade;
            set
            {
                if(value.IsAll)
                {
                    ClassList = new ObservableCollection<Lop>(_allClasses);
                    SelectedClass = ClassList[0];
                }
                else
                {
                    ObservableCollection<Lop> classes = new ObservableCollection<Lop>();
                    foreach (Lop @class in _allClasses)
                    {
                        if (@class.Makhoi == value.Makhoi || @class.IsAll)
                            classes.Add(@class);
                    }
                    ClassList = classes;
                    SelectedClass = ClassList[0];
                }
                    _selectedGrade = value;
                OnPropertyChanged(nameof(SelectedGrade));
            }
        }

        List<Lop> _allClasses;

        private Lop _selectedClass;
        private ObservableCollection<Lop> _classList;
        public ObservableCollection<Lop> ClassList 
        {
            get => _classList;
            set
            {
                _classList = value;
                OnPropertyChanged(nameof(ClassList));
            }
        }
        public Lop SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                OnPropertyChanged(nameof(SelectedClass));
            }
        }

        

        private string _selectedGender;
        public ObservableCollection<string> GenderList { get; } = new ObservableCollection<string> { "[Tất cả]","Nam", "Nữ" };
        public string SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
                OnPropertyChanged(nameof(SelectedGender));
            }
        } 


        private string _studentID;
        public string StudentID
        {
            get => _studentID;
            set
            {
                if (_studentID != value)
                {
                    _studentID = value;
                    OnPropertyChanged(nameof(StudentID));
                }
            }
        }
        private string _studentName;
        public string StudentName
        {
            get => _studentName;
            set
            {
                
                    _studentName = value;
                    OnPropertyChanged(nameof(StudentName));
               
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
        private StudentDisplayViewModel _selectedStudent;
        public StudentDisplayViewModel SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));
            }
        }



        private SchoolYearStore _schoolYearStore;
        private NavigationService _navigationService;
        private BaseViewModel _parent;

        public ICommand AddStudentCommand { get; }
        public ICommand ViewStudentCommand { get; }
        public ICommand ModifyStudentCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }
        public SearchStudentViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore, BaseViewModel parent)
        {

            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            _parent = parent;
            _gradeList = new ObservableCollection<Khoi>();
            _classList = new ObservableCollection<Lop>();
            _selectedStudents = new List<StudentDisplayViewModel>();
            _allClasses = new List<Lop>();
            _selectedGender = GenderList[0];
            AddStudentCommand = new RelayCommand(OpenAddStudentView);
            ViewStudentCommand = new RelayCommand(OpenViewStudentView);
            ModifyStudentCommand = new RelayCommand(OpenMidifyStudentView);
            DeleteCommand = new AsyncRelayCommand(DeleteStudent);
            SearchCommand = new AsyncRelayCommand(Search);
            _schoolYearStore.SelectedSchoolYearChanged += Update;

            Task.Run(LoadDataAsync).ConfigureAwait(false);
        }

        ~SearchStudentViewModel()
        {
            _schoolYearStore.SelectedSchoolYearChanged -= Update;
        }
        private void Update()
        {
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            
            using(var context = new QuanlyhocsinhContext())
            {
                List<Khoi> grades = await context.Khois.ToListAsync();
                Khoi allGrade = new Khoi();
                allGrade.IsAll = true;
                grades.Insert(0, allGrade);
                _selectedGrade = allGrade;
                GradeList = new ObservableCollection<Khoi>(grades);

                List<Lop> classes = await context.Lops
                                                .Where(l => l.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc)
                                                .OrderBy(l => l.Tenlop)
                                                .ToListAsync();
                _allClasses = classes;
                Lop allClass = new Lop();
                allClass.IsAll = true;
                _allClasses.Insert(0, allClass);
                _selectedClass = allClass;
                ClassList = new ObservableCollection<Lop>(_allClasses);
                List<Hocsinh> students;
                if (!_schoolYearStore.SelectedSchoolYear.IsAll)
                {
                    students = await context.Hocsinhs
                                                   .Where(hs => hs.CtLops.Any(ct => ct.MalopNavigation.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc))
                                                   .Include(hs => hs.CtLops.Where(ct => ct.MalopNavigation.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc))
                                                    .ThenInclude(ct => ct.MalopNavigation)
                                                    .ThenInclude(l => l.MakhoiNavigation)
                                                    .OrderBy(hs => hs.Mahs)
                                                   .ToListAsync();
                }
                else
                {
                    students = await context.Hocsinhs
                                                   .Include(hs => hs.CtLops)
                                                    .ThenInclude(ct => ct.MalopNavigation)
                                                    .ThenInclude(l => l.MakhoiNavigation)
                                                    .OrderBy(hs => hs.Mahs)
                                                   .ToListAsync();
                }

                    ObservableCollection<StudentDisplayViewModel> studentDisplayViewModels = new ObservableCollection<StudentDisplayViewModel>();
                foreach(Hocsinh student in students)
                {
                    studentDisplayViewModels.Add(new StudentDisplayViewModel(student));
                }
                Students = studentDisplayViewModels;
            }
        }
        private void OpenAddStudentView()
        {
            _navigationService.PushStack(_parent);
            AddStudentViewModel addStudentViewModel = new AddStudentViewModel(_navigationService, _schoolYearStore);
            Action<string> handler = null;
                handler = (id) =>
            {
                AddStudent(id);
                addStudentViewModel.AddStudent -= handler;
            };
            addStudentViewModel.AddStudent += handler;

            _navigationService.Navigate(addStudentViewModel);
        }

        private async Task AddStudent(string id)
        {
            using(var context = new QuanlyhocsinhContext())
            {
                Hocsinh student = await context.Hocsinhs
                    .Include(hs => hs.CtLops)
                                                    .ThenInclude(ct => ct.MalopNavigation)
                                                    .ThenInclude(l => l.MakhoiNavigation)
                    .FirstOrDefaultAsync(hs => hs.Mahs == id);
                Students.Insert(0,new StudentDisplayViewModel(student));
            }
        }
        private void OpenViewStudentView()
        {
            if(_selectedStudent != null)
            {
                _navigationService.PushStack(_parent);
               
                _navigationService.Navigate(new ViewStudentViewModel(_navigationService,_selectedStudent.Student));
            }
            
        }


        private void OpenMidifyStudentView()
        {
            if (_selectedStudent != null)
            {
                _navigationService.PushStack(_parent);
                ModifyStudentViewModel modifyStudentViewModel = new ModifyStudentViewModel(_navigationService, _schoolYearStore, _selectedStudent.Student);
                Action<string> handler = null;
                handler = (id) =>

                {
                    ModifyStudent(id);
                    modifyStudentViewModel.ModifyStudent -= handler;
                };
                modifyStudentViewModel.ModifyStudent += handler;
                _navigationService.Navigate(modifyStudentViewModel);
            }
        }

        private async Task ModifyStudent(string id)
        {
            using (var context = new QuanlyhocsinhContext())
            {
                Hocsinh student = await context.Hocsinhs
                    .Include(hs => hs.CtLops)
                                                    .ThenInclude(ct => ct.MalopNavigation)
                                                    .ThenInclude(l => l.MakhoiNavigation)
                    .FirstOrDefaultAsync(hs => hs.Mahs == id);
                int pos = Students.IndexOf(SelectedStudent);
                Students.RemoveAt(pos);
                Students.Insert(pos, new StudentDisplayViewModel(student));
            }
        }
      

        private IList _selectedStudents;
        public IList SelectedStudents
        {
            get => _selectedStudents;
            set
            {
                _selectedStudents = value;
                OnPropertyChanged(nameof(SelectedStudents));
            }
        }
        private async Task DeleteStudent()
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa học sinh?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new QuanlyhocsinhContext())
                    {
                        List<StudentDisplayViewModel> studentDisplayViewModels = _selectedStudents.Cast<StudentDisplayViewModel>().ToList();
                        foreach (StudentDisplayViewModel studentDisplayViewModel in studentDisplayViewModels)
                        {
                            Hocsinh student = await context.Hocsinhs
                                .Include(hs => hs.CtBangdiemmonhocs)
                                .Include(hs => hs.CtLops)
                                .FirstOrDefaultAsync(hs => hs.Mahs == studentDisplayViewModel.Student.Mahs);
                            if (student.CtBangdiemmonhocs.Count > 0)
                                throw new Exception("Học sinh đã có dữ liệu điểm không thể xóa");
                            if (student.CtLops.Count > 0)
                            {
                                foreach(CtLop ctLop in student.CtLops)
                                    context.CtLops.Remove(ctLop);

                            }
                            else
                                context.Hocsinhs.Remove(student);
                        }
                        await context.SaveChangesAsync();

                        foreach (StudentDisplayViewModel studentDisplayViewModel in studentDisplayViewModels)
                        {

                            Students.Remove(studentDisplayViewModel);
                        }
                        OnPropertyChanged(nameof(Students));
                    }
                }
            }
            catch (DbUpdateException ex)
            {

                MessageBox.Show("Tồn tại dữ liệu liên quan học sinh không thể xóa");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
           
           
        }

        public async Task Search()
        {
            try
            {
                using (var context = new QuanlyhocsinhContext())
                {
                    IQueryable<Hocsinh> query = context.Hocsinhs

                                                    .Include(hs => hs.CtLops)
                                                    .ThenInclude(ct => ct.MalopNavigation)
                                                    .ThenInclude(l => l.MakhoiNavigation);
                    if (!_schoolYearStore.SelectedSchoolYear.IsAll)
                        query = query.Where(hs => hs.CtLops.Any(ct => ct.MalopNavigation.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc));

                    if (!string.IsNullOrWhiteSpace(_studentID))
                        query = query.Where(hs => EF.Functions.Like(hs.Mahs, "%" + _studentID + "%"));
                    if (!string.IsNullOrWhiteSpace(_studentName))
                        query = query.Where(hs => EF.Functions.Like(hs.Hoten, "%" + _studentName + "%"));
                    
                    if(_selectedGrade.IsAll)
                    {
                        if (!_selectedClass.IsAll)
                            query = query.Where(hs => hs.CtLops.Any(ct => ct.Malop == _selectedClass.Malop));
                    }         
                    else
                    {
                        if (!_selectedClass.IsAll)
                            query = query.Where(hs => hs.CtLops.Any(ct => ct.Malop == _selectedClass.Malop));
                        else
                            query = query.Where(hs => hs.CtLops.Any(ct => ct.MalopNavigation.Makhoi == _selectedGrade.Makhoi));

                    }
                    if (_selectedGender != "[Tất cả]")
                            query = query.Where(hs => hs.Gioitinh == _selectedGender);


                    List<Hocsinh> students = await query.OrderBy(hs => hs.Mahs).ToListAsync();

                    ObservableCollection<StudentDisplayViewModel> studentDisplayViewModels = new ObservableCollection<StudentDisplayViewModel>();
                    foreach (Hocsinh student in students)
                    {
                        studentDisplayViewModels.Add(new StudentDisplayViewModel(student));
                    }
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

        //private void UpdateIndexes()
        //{
        //    for (int i = 0; i < Students.Count; i++)
        //    {
        //        Students[i].Index = i + 1;
        //    }
        //}
        //public ICommand DeleteCommand { get; }
        //private void SearchStudents()
        //{
        //    var criteria = new SearchCriteria
        //    {
        //        Grade = SelectedGrade,
        //        ClassName = SelectedClass,
        //        Gender = SelectedGender,
        //        Status = SelectedStatus,
        //        Ethnicity = SelectedEthnicity,
        //        FullName = FullName,
        //        StudentID = StudentID
        //    };

        //    var result = studentService.SearchStudents(criteria);

        //    Students.Clear();
        //    foreach (var s in result)
        //    {
        //        Students.Add(s);
        //    }

        //    UpdateIndexes();
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


        //private void Student_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(StudentDisplayModel.IsSelected))
        //    {
        //        UpdateIsAllSelected();
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

        //private List<string> deletedStudentIds = new List<string>();


        //private void DeleteStudent()
        //{
        //    var hocsinh = Students.Where(hs => hs.IsSelected).ToList();
        //    var filterhocsinh = Students.Where(hs => !hs.IsSelected).ToList();
        //    foreach (var hs in hocsinh)
        //    {
        //        deletedStudentIds.Add(hs.ID);  
        //        Students.Remove(hs);
        //    }

        //    Students.Clear();
        //    foreach (var s in filterhocsinh)
        //    {
        //        Students.Add(s);
        //    }
        //    UpdateIndexes();
        //}

        //private void CancelSearch()
        //{
        //    SelectedGrade = null;
        //    SelectedClass = null;
        //    SelectedGender = null;
        //    SelectedStatus = null;
        //    SelectedEthnicity = null;
        //    StudentID = null;
        //    FullName = null;
        //    FilteredClassList = new List<string>();
        //    IsAllSelected = false;

        //    var allStudents = studentService.SearchStudents(new SearchCriteria());
        //    var filteredStudents = allStudents
        //        .Where(s => !deletedStudentIds.Contains(s.ID))
        //        .ToList();

        //    Students.Clear();
        //    foreach (var s in filteredStudents)
        //    {
        //        Students.Add(s);
        //    }

        //    UpdateIndexes();
        //}
    }
}
