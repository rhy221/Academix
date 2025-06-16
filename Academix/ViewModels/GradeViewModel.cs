using Academix.Models;
using Academix.Models.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;

namespace Academix.ViewModels
{
    class GradeViewModel : BaseViewModel
    {
        public ObservableCollection<StudentScoreDisplay> Students { get; set; } = new();

        public ObservableCollection<StudentScoreDisplay> FilteredStudents { get; set; } = new();
        
        #region Properties for Filters
        
        private string selectedGrade;
        public List<string> GradeList { get; } = new List<string> { "10", "11", "12" };
        public string SelectedGrade
        {
            get => selectedGrade;
            set
            {
                if (selectedGrade != value)
                {
                    selectedGrade = value;
                    NumberOfScoreColumns = 1;
                    OnPropertyChanged(nameof(SelectedGrade));
                    UpdateClassList();
                }
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

        private string selectedClass;
        public List<string> ClassList { get; } = new List<string> { "10/1", "10/2", "10/3", "11/1", "11/2", "11/3", "12/1", "12/2", "12/3" };
        public string SelectedClass
        {
            get => selectedClass;
            set
            {
                if (selectedClass != value)
                {
                    selectedClass = value;
                    NumberOfScoreColumns = 1;
                    OnPropertyChanged(nameof(SelectedClass));
                    ApplyFilter();
                }
            }
        }

        private string selectedLoaiDiem;
        public List<string> LoaiDiemList { get; } = new List<string> { "Điểm miệng", "Điểm 15 phút", "Điểm 1 tiết", "Điểm cuối kỳ" };
        public string SelectedLoaiDiem
        {
            get => selectedLoaiDiem;
            set
            {
                if (selectedLoaiDiem != value)
                {
                    selectedLoaiDiem = value;
                    NumberOfScoreColumns = 1;
                    UpdateNumericUpDownState();
                    OnPropertyChanged(nameof(SelectedLoaiDiem));
                    ApplyFilter();
                }
            }
        }

        private string selectedSemester;
        public List<string> SemesterList { get; } = new List<string> { "Học kỳ 1", "Học kỳ 2" };
        public string SelectedSemester
        {
            get => selectedSemester;
            set
            {
                if (selectedSemester != value)
                {
                    selectedSemester = value;
                    NumberOfScoreColumns = 1;
                    OnPropertyChanged(nameof(SelectedSemester));
                    ApplyFilter();
                }
            }
        }
        
        private string selectedSubject;
        public List<string> SubjectList { get; } = new List<string> { "Toán", "Lý", "Hóa", "Sinh", "Sử", "Địa", "Văn", "Tiếng Anh", "Giáo dục công dân" };
        public string SelectedSubject
        {
            get => selectedSubject;
            set
            {
                if (selectedSubject != value)
                {
                    selectedSubject = value;
                    NumberOfScoreColumns = 1;
                    OnPropertyChanged(nameof(SelectedSubject));
                    ApplyFilter();
                }
            }
        }

        private void UpdateIndexes()
        {
            for (int i = 0; i < FilteredStudents.Count; i++)
            {
                FilteredStudents[i].Index = i + 1;
            }
        }


        private bool _isNumericUpDownEnabled = true;
        public bool IsNumericUpDownEnabled
        {
            get => _isNumericUpDownEnabled;
            set
            {
                if (_isNumericUpDownEnabled != value)
                {
                    _isNumericUpDownEnabled = value;
                    OnPropertyChanged(nameof(IsNumericUpDownEnabled));
                }
            }
        }
        private void UpdateNumericUpDownState()
        {
            if (SelectedLoaiDiem == "Điểm cuối kỳ")
            {
                IsNumericUpDownEnabled = false;
            }
            else
            {
                IsNumericUpDownEnabled = true;
            }
        }


        private int numberOfScoreColumns = 1;
        public int NumberOfScoreColumns
        {
            get => numberOfScoreColumns;
            set
            {
                // Lưu giá trị cũ để so sánh
                int oldValue = numberOfScoreColumns;

                // Nếu giá trị không đổi thì không làm gì
                if (value == oldValue) return;

                // --- LOGIC GIẢM SỐ CỘT ---
                if (value < oldValue)
                {
                    // Vị trí (index) của cột điểm sắp bị xóa
                    int columnIndexToRemove = value;

                    // Tìm các học sinh đã có điểm ở cột này
                    var studentsWithScore = FilteredStudents
                        .Where(s => s.Scores.Count > columnIndexToRemove && s.Scores[columnIndexToRemove].HasValue)
                        .ToList();

                    if (studentsWithScore.Any())
                    {
                        // Nếu có học sinh đã nhập điểm, hiển thị thông báo và không cho giảm
                        var studentNames = string.Join(", ", studentsWithScore.Select(s => s.Name));
                        MessageBox.Show($"Không thể giảm số lần điểm. Các học sinh sau đã có điểm ở cột này: {studentNames}.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);

                        // Trả lại giá trị cũ cho NumericUpDown trên UI
                        OnPropertyChanged(nameof(NumberOfScoreColumns)); // Báo cho UI rằng giá trị đã bị "khôi phục"
                        return; // Dừng thực thi
                    }
                }

                // Nếu điều kiện giảm được thỏa mãn, hoặc nếu là tăng, thì tiến hành cập nhật
                {
                    if (numberOfScoreColumns != value)
                    {
                        numberOfScoreColumns = value;
                        OnPropertyChanged(nameof(NumberOfScoreColumns));
                    }
                }

                // Cập nhật lại kích thước của các collection Scores
                foreach (var student in FilteredStudents)
                {
                    // Tăng: Thêm các giá trị null
                    while (student.Scores.Count < numberOfScoreColumns)
                    {
                        student.Scores.Add(null);
                    }
                    // Giảm: Xóa các phần tử cuối
                    while (student.Scores.Count > numberOfScoreColumns)
                    {
                        student.Scores.RemoveAt(student.Scores.Count - 1);
                    }
                }


            }
        }

        #endregion

        public ICommand SaveChangesCommand { get; }

        public GradeViewModel()
        {
            var studentsData = new ObservableCollection<StudentScoreDisplay>();
            var random = new Random();
            List<SemesterScore> CreateSampleScores()
            {
                return new List<SemesterScore>
                {
                    new SemesterScore { Semester = Semester.Semester1, Subjects = new List<SubjectScore> { new SubjectScore { Subject = SubjectType.Toan, ScoreGroups = new List<ScoreGroup> { new ScoreGroup { ScoreType = ScoreType.Oral, Scores = new List<Score> { new Score { Value = random.Next(5, 11) } } }, new ScoreGroup { ScoreType = ScoreType.Test15Min, Scores = new List<Score> { new Score { Value = Math.Round(random.NextDouble() * 5 + 5, 1) } } }, new ScoreGroup { ScoreType = ScoreType.Test1Period, Scores = new List<Score> { new Score { Value = Math.Round(random.NextDouble() * 5 + 5, 1) } } }, new ScoreGroup { ScoreType = ScoreType.Final, Scores = new List<Score> { new Score { Value = Math.Round(random.NextDouble() * 5 + 5, 1) } } } } } } }
                };
            }
            studentsData.Add(new StudentScoreDisplay { ID = "HS1001", Name = "Nguyễn Văn An", Class = "10/1", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1002", Name = "Trần Thị Bình", Class = "10/1", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1003", Name = "Lê Hoàng Cường", Class = "10/1", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1004", Name = "Phạm Thị Dung", Class = "10/2", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1005", Name = "Võ Minh Đức", Class = "10/2", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1006", Name = "Hoàng Ngọc Hà", Class = "10/2", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1007", Name = "Phan Thanh Hùng", Class = "10/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1008", Name = "Đặng Thị Lan", Class = "10/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1009", Name = "Bùi Việt Long", Class = "10/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1010", Name = "Vũ Thị Mai", Class = "10/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1101", Name = "Nguyễn Thu Thảo", Class = "11/1", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1102", Name = "Trần Quốc Tuấn", Class = "11/1", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1103", Name = "Lê Gia Huy", Class = "11/1", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1104", Name = "Phạm Khánh Linh", Class = "11/2", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1105", Name = "Võ Hoàng Nam", Class = "11/2", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1106", Name = "Hoàng Bảo Ngọc", Class = "11/2", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1107", Name = "Phan Minh Quân", Class = "11/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1108", Name = "Đặng Thùy Chi", Class = "11/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1109", Name = "Bùi Đức Anh", Class = "11/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1110", Name = "Vũ Phương Anh", Class = "11/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1201", Name = "Nguyễn Nhật Minh", Class = "12/1", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1202", Name = "Trần Ngọc Ánh", Class = "12/1", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1203", Name = "Lê Quang Vinh", Class = "12/1", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1204", Name = "Phạm Tuấn Kiệt", Class = "12/2", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1205", Name = "Võ Thanh Trúc", Class = "12/2", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1206", Name = "Hoàng Minh Hiếu", Class = "12/2", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1207", Name = "Phan Hà My", Class = "12/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1208", Name = "Đặng Quang Khải", Class = "12/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1209", Name = "Bùi Thảo Vy", Class = "12/3", AllSemesterScores = CreateSampleScores() });
            studentsData.Add(new StudentScoreDisplay { ID = "HS1210", Name = "Vũ Hải Đăng", Class = "12/3", AllSemesterScores = CreateSampleScores() });
            this.Students = studentsData;

            // Thiết lập các giá trị tĩnh trước ---
            selectedSemester = "Học kỳ 1";
            selectedSubject = "Toán";
            selectedLoaiDiem = "Điểm miệng";
            SelectedGrade = "10";
            SaveChangesCommand = new RelayCommand(SaveChanges);
        }

        private void UpdateClassList()
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

            SelectedClass = FilteredClassList.FirstOrDefault();
        }


        private void ApplyFilter()
        {

            if (string.IsNullOrEmpty(SelectedClass) ||
                string.IsNullOrEmpty(SelectedSemester) ||
                string.IsNullOrEmpty(SelectedSubject) ||
                string.IsNullOrEmpty(SelectedLoaiDiem))
            {
                FilteredStudents.Clear();
                return;
            }

            var semesterEnum = ConvertToSemester(SelectedSemester);
            var subjectEnum = ConvertToSubjectType(SelectedSubject);
            var scoreTypeEnum = ConvertToScoreType(SelectedLoaiDiem);

            var studentsInClass = Students.Where(s => s.Class == SelectedClass).ToList();

            FilteredStudents.Clear();
            foreach (var student in studentsInClass)
            {
                var originalScores = student.GetScores(semesterEnum, subjectEnum, scoreTypeEnum);

                if (student.Scores == null)
                {
                    student.Scores = new ObservableCollection<double?>();
                }
                else
                {
                    student.Scores.Clear();
                }

                if (originalScores != null)
                {
                    foreach (var score in originalScores)
                    {
                        student.Scores.Add(score);
                    }
                }
                while (student.Scores.Count < NumberOfScoreColumns)
                {
                    student.Scores.Add(null);
                }
                FilteredStudents.Add(student);
            }

            UpdateIndexes();
        }


        #region Helper Methods for Enum Conversion

        private Semester ConvertToSemester(string semester)
        {
            return semester == "Học kỳ 1" ? Semester.Semester1 : Semester.Semester2;
        }

        private SubjectType ConvertToSubjectType(string subject)
        {
            switch (subject)
            {
                case "Toán": return SubjectType.Toan;
                case "Lý": return SubjectType.Ly;
                case "Hóa": return SubjectType.Hoa;
                case "Sinh": return SubjectType.Sinh;
                case "Sử": return SubjectType.Su;
                case "Địa": return SubjectType.Dia;
                case "Văn": return SubjectType.Van;
                case "Tiếng Anh": return SubjectType.Anh;
                case "Giáo dục công dân": return SubjectType.GDCD;
                default:
                    // Trả về một giá trị mặc định hoặc ném ra lỗi nếu cần
                    throw new ArgumentOutOfRangeException(nameof(subject), $"Môn học không hợp lệ: {subject}");
            }
        }

        private ScoreType ConvertToScoreType(string scoreType)
        {
            switch (scoreType)
            {
                case "Điểm miệng": return ScoreType.Oral;
                case "Điểm 15 phút": return ScoreType.Test15Min;
                case "Điểm 1 tiết": return ScoreType.Test1Period;
                case "Điểm cuối kỳ": return ScoreType.Final;
                default:
                    // Trả về một giá trị mặc định hoặc ném ra lỗi nếu cần
                    throw new ArgumentOutOfRangeException(nameof(scoreType), $"Loại điểm không hợp lệ: {scoreType}");
            }
        }

        private void SaveChanges()
        {
            try
            {
                var semesterEnum = ConvertToSemester(SelectedSemester);
                var subjectEnum = ConvertToSubjectType(SelectedSubject);
                var scoreTypeEnum = ConvertToScoreType(SelectedLoaiDiem);

                foreach (var student in FilteredStudents)
                {
                    if (student.Scores.Any(s => s.HasValue && (s < 0 || s > 10)))
                    {
                        MessageBox.Show(
                            $"Học sinh {student.Name} có điểm không hợp lệ (0-10).",
                            "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    student.UpdateScores(semesterEnum, subjectEnum, scoreTypeEnum);
                }

                var message = "Đã lưu điểm thành công!\n\n";
                foreach (var student in FilteredStudents)
                {
                    var scoresText = string.Join(" ", student.Scores.Select(s => s.HasValue ? s.Value.ToString() : "null"));
                    message += $"{student.Name}: {scoresText}\n";
                }

                MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi lưu điểm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}