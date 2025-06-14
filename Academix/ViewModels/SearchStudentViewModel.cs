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

namespace Academix.ViewModels
{
    public class SearchStudentViewModel : BaseViewModel
    {
        private string selectedGrade;
        public List<string> GradeList { get; } = new List<string> { "10", "11", "12" };
        public string SelectedGrade
        {
            get => selectedGrade;
            set
            {
                selectedGrade = value;
                OnPropertyChanged(nameof(SelectedGrade));
                FilterClassByGrade();
            }
        }

        private string selectedClass;
        public List<string> ClassList { get; } = new List<string> { "10/1", "10/2", "10/3",
                                                                    "11/1", "11/2", "11/3",
                                                                    "12/1", "12/2", "12/3"};
        public string SelectedClass
        {
            get => selectedClass;
            set
            {
                selectedClass = value;
                OnPropertyChanged(nameof(SelectedClass));
            }
        }
        private List<string> filteredClassList;
        public List<string> FilteredClassList
        {
            get => filteredClassList;
            set
            {
                filteredClassList = value;
                OnPropertyChanged(nameof(FilteredClassList));
            }
        }
        private void FilterClassByGrade()
        {
            if (string.IsNullOrEmpty(SelectedGrade))
            {
                FilteredClassList = new List<string>();
            }
            else
            {
                FilteredClassList = ClassList
                    .Where(cls => cls.StartsWith(SelectedGrade))
                    .ToList();
            }

            SelectedClass = null;
        }

        private string selectedStatus;
        public List<string> StatusList { get; } = new List<string> { "Đang học", "Nghỉ học" };

        public string SelectedStatus
        {
            get => selectedStatus;
            set
            {
                if (selectedStatus != value)
                {
                    selectedStatus = value;
                    OnPropertyChanged(nameof(SelectedStatus));
                }
            }
        }

        private string selectedGender;
        public List<string> GenderList { get; } = new List<string> { "Nam", "Nữ" };
        public string SelectedGender
        {
            get => selectedGender;
            set
            {
                selectedGender = value;
                OnPropertyChanged(nameof(SelectedGender));
            }
        }

        private string selectedEthnicity;
        public List<string> EthnicityList { get; } = new List<string>
        {
            "Ba Na", "Bố Y", "Brâu", "Bru - Vân Kiều", "Chăm",
            "Chơ Ro", "Chứt", "Co", "Cơ Tu", "Cống",
            "Cờ Lao", "Dao", "Ê Đê", "Giáy", "Gia Rai",
            "Hà Nhì", "Hrê", "H'Mông", "Kháng", "Khơ Mú",
            "Khmer", "Kinh", "La Chí", "La Ha", "La Hủ",
            "Lào", "Lô Lô", "Lự", "Mạ", "Mảng",
            "Mnông", "Mường", "Ngái", "Nùng", "Ô Đu",
            "Pà Thẻn", "Phù Lá", "Pu Péo", "Ra Glai", "Rơ Măm",
            "Sán Chay", "Sán Dìu", "Si La", "Stiêng", "Tà Ôi",
            "Tày", "Thái", "Thổ", "Xê Đăng", "Xinh Mun",
            "Xơ Đăng", "Yến", "Dao Đỏ", "Khác"
        };
        public string SelectedEthnicity
        {
            get => selectedEthnicity;
            set
            {
                selectedEthnicity = value;
                OnPropertyChanged(nameof(SelectedEthnicity));
            }
        }

        private string studentID;
        public string StudentID
        {
            get => studentID;
            set
            {
                if (studentID != value)
                {
                    studentID = value;
                    OnPropertyChanged(nameof(StudentID));
                }
            }
        }
        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public IRelayCommand SearchCommand { get; }
        public IRelayCommand CancelSearchCommand { get; }
        private StudentSearchService studentService = new StudentSearchService();

        public ObservableCollection<StudentDisplayModel> Students { get; set; } = new();
        public SearchStudentViewModel()
        {

            studentService = new StudentSearchService();
            SearchCommand = new RelayCommand(SearchStudents);

            var all = studentService.SearchStudents(new SearchCriteria());
            Students = new ObservableCollection<StudentDisplayModel>(all);


            Students.CollectionChanged += (s, e) => UpdateIndexes();

            Students = new ObservableCollection<StudentDisplayModel>
            {
                new StudentDisplayModel(
                    new Student("HS001", "Nguyễn Văn A", true, new DateTime(2007, 5, 10), "123 Đường A", "a@example.com", null),
                    "10/1", 8.2, 8.5, "Đang học", 1, false),

                new StudentDisplayModel(
                    new Student("HS002", "Trần Thị B", false, new DateTime(2007, 8, 15), "456 Đường B", "b@example.com", null),
                    "10/1", 7.5, 7.8, "Đang học", 2, false),

                new StudentDisplayModel(
                    new Student("HS003", "Lê Văn C", true, new DateTime(2007, 2, 20), "789 Đường C", "c@example.com", null),
                    "10/2", 6.0, 6.5, "Nghỉ học", 3, false),

                new StudentDisplayModel(
                    new Student("HS004", "Phạm Thị D", false, new DateTime(2006, 11, 5), "101 Đường D", "d@example.com", null),
                    "11/1", 9.1, 9.4, "Đang học", 4, false),

                new StudentDisplayModel(
                    new Student("HS005", "Đặng Văn E", true, new DateTime(2006, 7, 25), "202 Đường E", "e@example.com", null),
                    "11/2", 5.0, 4.8, "Nghỉ học", 5, false),

                new StudentDisplayModel(
                    new Student("HS006", "Hoàng Thị F", false, new DateTime(2006, 3, 12), "303 Đường F", "f@example.com", null),
                    "11/3", 6.2, 6.9, "Đang học", 6, false),

                new StudentDisplayModel(
                    new Student("HS007", "Mai Văn G", true, new DateTime(2005, 10, 30), "404 Đường G", "g@example.com", null),
                    "12/1", 8.0, 8.3, "Đang học", 7, false),

                new StudentDisplayModel(
                    new Student("HS008", "Ngô Thị H", false, new DateTime(2005, 4, 18), "505 Đường H", "h@example.com", null),
                    "12/1", 9.0, 9.5, "Đang học", 8, false),

                new StudentDisplayModel(
                    new Student("HS009", "Bùi Văn I", true, new DateTime(2005, 6, 8), "606 Đường I", "i@example.com", null),
                    "12/2", 7.2, 7.1, "Nghỉ học", 9, false),

                new StudentDisplayModel(
                    new Student("HS010", "Đỗ Thị K", false, new DateTime(2005, 9, 22), "707 Đường K", "k@example.com", null),
                    "12/3", 8.5, 8.6, "Đang học", 10, false),

                new StudentDisplayModel(
                    new Student("HS011", "Trịnh Văn L", true, new DateTime(2007, 1, 14), "808 Đường L", "l@example.com", null),
                    "10/1", 5.5, 5.8, "Đang học", 11, false),

                new StudentDisplayModel(
                    new Student("HS012", "Phan Thị M", false, new DateTime(2006, 2, 27), "909 Đường M", "m@example.com", null),
                    "11/1", 7.8, 7.7, "Đang học", 12, false),

                new StudentDisplayModel(
                    new Student("HS013", "Vũ Văn N", true, new DateTime(2006, 5, 3), "111 Đường N", "n@example.com", null),
                    "11/2", 6.3, 6.2, "Nghỉ học", 13, false),

                new StudentDisplayModel(
                    new Student("HS014", "Lý Thị O", false, new DateTime(2007, 12, 19), "222 Đường O", "o@example.com", null),
                    "10/2", 9.0, 8.9, "Đang học", 14, false),

                new StudentDisplayModel(
                    new Student("HS015", "Nguyễn Văn P", true, new DateTime(2005, 8, 7), "333 Đường P", "p@example.com", null),
                    "12/3", 7.0, 7.4, "Đang học", 15, false)
            };

            foreach (var student in Students)
            {
                student.PropertyChanged += Student_PropertyChanged;
            }

            DeleteCommand = new RelayCommand(DeleteStudent);
            CancelSearchCommand = new RelayCommand(CancelSearch);

            UpdateIndexes();

        }

        private void UpdateIndexes()
        {
            for (int i = 0; i < Students.Count; i++)
            {
                Students[i].Index = i + 1;
            }
        }
        public ICommand DeleteCommand { get; }
        private void SearchStudents()
        {
            var criteria = new SearchCriteria
            {
                Grade = SelectedGrade,
                ClassName = SelectedClass,
                Gender = SelectedGender,
                Status = SelectedStatus,
                Ethnicity = SelectedEthnicity,
                FullName = FullName,
                StudentID = StudentID
            };

            var result = studentService.SearchStudents(criteria);

            Students.Clear();
            foreach (var s in result)
            {
                Students.Add(s);
            }

            UpdateIndexes();
        }
        private bool? _isAllSelected = false;
        private bool _isUpdatingFromStudents = false;

        public bool? IsAllSelected
        {
            get => _isAllSelected;
            set
            {
                if (_isAllSelected == value)
                    return;

                _isAllSelected = value;
                OnPropertyChanged(nameof(IsAllSelected));

                if (value.HasValue && !_isUpdatingFromStudents)
                {
                    foreach (var student in Students)
                    {
                        student.IsSelected = value.Value;
                    }
                }
            }
        }


        private void Student_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StudentDisplayModel.IsSelected))
            {
                UpdateIsAllSelected();
            }
        }

        private void UpdateIsAllSelected()
        {
            _isUpdatingFromStudents = true;

            var selectedCount = Students.Count(s => s.IsSelected);
            if (selectedCount == Students.Count)
            {
                IsAllSelected = true;
            }
            else if (selectedCount == 0)
            {
                IsAllSelected = false;
            }
            else
            {
                IsAllSelected = false;
            }

            _isUpdatingFromStudents = false;
        }

        private List<string> deletedStudentIds = new List<string>();


        private void DeleteStudent()
        {
            var hocsinh = Students.Where(hs => hs.IsSelected).ToList();
            var filterhocsinh = Students.Where(hs => !hs.IsSelected).ToList();
            foreach (var hs in hocsinh)
            {
                deletedStudentIds.Add(hs.ID);  
                Students.Remove(hs);
            }

            Students.Clear();
            foreach (var s in filterhocsinh)
            {
                Students.Add(s);
            }
            UpdateIndexes();
        }

        private void CancelSearch()
        {
            SelectedGrade = null;
            SelectedClass = null;
            SelectedGender = null;
            SelectedStatus = null;
            SelectedEthnicity = null;
            StudentID = null;
            FullName = null;
            FilteredClassList = new List<string>();
            IsAllSelected = false;

            var allStudents = studentService.SearchStudents(new SearchCriteria());
            var filteredStudents = allStudents
                .Where(s => !deletedStudentIds.Contains(s.ID))
                .ToList();

            Students.Clear();
            foreach (var s in filteredStudents)
            {
                Students.Add(s);
            }

            UpdateIndexes();
        }
    }
}
