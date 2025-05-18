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
                new StudentDisplayModel { ID = "HS001", Name = "Nguyễn Văn A", Gender = "Nam", ClassName = "10/1", GPA1 = 8.2, GPA2 = 8.5, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS002", Name = "Trần Thị B", Gender = "Nữ", ClassName = "10/1", GPA1 = 7.5, GPA2 = 7.8, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS003", Name = "Lê Văn C", Gender = "Nam", ClassName = "10/2", GPA1 = 6.0, GPA2 = 6.5, Status = "Nghỉ học"},
                new StudentDisplayModel { ID = "HS004", Name = "Phạm Thị D", Gender = "Nữ", ClassName = "11/1", GPA1 = 9.1, GPA2 = 9.4, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS005", Name = "Đặng Văn E", Gender = "Nam", ClassName = "11/2", GPA1 = 5.0, GPA2 = 4.8, Status = "Nghỉ học" },
                new StudentDisplayModel { ID = "HS006", Name = "Hoàng Thị F", Gender = "Nữ", ClassName = "11/3", GPA1 = 6.2, GPA2 = 6.9, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS007", Name = "Mai Văn G", Gender = "Nam", ClassName = "12/1", GPA1 = 8.0, GPA2 = 8.3, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS008", Name = "Ngô Thị H", Gender = "Nữ", ClassName = "12/1", GPA1 = 9.0, GPA2 = 9.5, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS009", Name = "Bùi Văn I", Gender = "Nam", ClassName = "12/2", GPA1 = 7.2, GPA2 = 7.1, Status = "Nghỉ học"},
                new StudentDisplayModel { ID = "HS010", Name = "Đỗ Thị K", Gender = "Nữ", ClassName = "12/3", GPA1 = 8.5, GPA2 = 8.6, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS011", Name = "Trịnh Văn L", Gender = "Nam", ClassName = "10/1", GPA1 = 5.5, GPA2 = 5.8, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS012", Name = "Phan Thị M", Gender = "Nữ", ClassName = "11/1", GPA1 = 7.8, GPA2 = 7.7, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS013", Name = "Vũ Văn N", Gender = "Nam", ClassName = "11/2", GPA1 = 6.3, GPA2 = 6.2, Status = "Nghỉ học" },
                new StudentDisplayModel { ID = "HS014", Name = "Lý Thị O", Gender = "Nữ", ClassName = "10/2", GPA1 = 9.0, GPA2 = 8.9, Status = "Đang học"},
                new StudentDisplayModel { ID = "HS015", Name = "Nguyễn Văn P", Gender = "Nam", ClassName = "12/3", GPA1 = 7.0, GPA2 = 7.4, Status = "Đang học"},
            };

            SearchCommand = new RelayCommand(SearchStudents);

            UpdateIndexes();

        }

        private void UpdateIndexes()
        {
            for (int i = 0; i < Students.Count; i++)
            {
                Students[i].Index = i + 1;
            }
        }

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

    }
}
