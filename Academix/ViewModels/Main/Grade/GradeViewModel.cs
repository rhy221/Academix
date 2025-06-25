using Academix.Models;
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
using Academix.Services;
using Academix.Stores;
using Microsoft.EntityFrameworkCore;
using Academix.Models.Main;
using Academix.DbContexts;

namespace Academix.ViewModels.Main.Grade
{
    class GradeViewModel : BaseViewModel
    {
        private ObservableCollection<StudentScoreDisplay> _filteredStudents;
        public ObservableCollection<StudentScoreDisplay> FilteredStudents {
            get => _filteredStudents;
            set
            {
                _filteredStudents = value;
                OnPropertyChanged(nameof(FilteredStudents));
            } } 
        private NavigationService _navigationService;
        private SchoolYearStore _schoolYearStore;

        //#region Properties for Filters

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
                if (_selectedGrade != value)
                {
                    _selectedGrade = value;
                    No = 1;
                    OnPropertyChanged(nameof(SelectedGrade));
                    UpdateClassList();
                }
            }
        }

        private ObservableCollection<Lop> _filteredClassList;
        public ObservableCollection<Lop> FilteredClassList
        {
            get => _filteredClassList;
            set
            {
                _filteredClassList = value;
                OnPropertyChanged(nameof(FilteredClassList));
            }
        }

        private Lop _selectedClass;
        public List<Lop> _classList;
        public Lop SelectedClass
        {
            get => _selectedClass;
            set
            {
                if (_selectedClass != value)
                {
                    _selectedClass = value;
                    No = 1;
                    OnPropertyChanged(nameof(SelectedClass));
                    ApplyFilter();
                }
            }
        }

        private Loaidiem _selectedScoreType;
        private ObservableCollection<Loaidiem> _scoreTypes;
        public ObservableCollection<Loaidiem> ScoreTypeList {
            get => _scoreTypes;
            set
            {
                _scoreTypes = value;
                OnPropertyChanged(nameof(ScoreTypeList));
            }
        }
        public Loaidiem SelectedScoreType
        {
            get => _selectedScoreType;
            set
            {
                if (_selectedScoreType != value)
                {
                    _selectedScoreType = value;
                    No = 1;
                    //UpdateNumericUpDownState();
                    OnPropertyChanged(nameof(SelectedScoreType));
                    ApplyFilter();
                }
            }
        }

        private Hocky _selectedSemester;
        private ObservableCollection<Hocky> _semesterList;
        public ObservableCollection<Hocky> SemesterList {
            get => _semesterList;
            set
            {
                _semesterList = value;
                OnPropertyChanged(nameof(SemesterList));
            }
        } 

        public Hocky SelectedSemester
        {
            get => _selectedSemester;
            set
            {
                if (_selectedSemester != value)
                {
                    _selectedSemester = value;
                    //NumberOfScoreColumns = 1;
                    OnPropertyChanged(nameof(SelectedSemester));
                    ApplyFilter();
                }
            }
        }

        private Monhoc _selectedSubject;
        private ObservableCollection<Monhoc> _subjectList;
        public ObservableCollection<Monhoc>  SubjectList
            {
            get => _subjectList;
            set
            {
                _subjectList = value;
                OnPropertyChanged(nameof(SubjectList));

            }
            }

        public Monhoc SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                if (_selectedSubject != value)
                {
                    _selectedSubject = value;
                    //NumberOfScoreColumns = 1;
                    OnPropertyChanged(nameof(SelectedSubject));
                    ApplyFilter();
                }
            }
        }

        private int _no;
        public int No
        {
            get => _no;
            set
            {
                _no = value;
                OnPropertyChanged(nameof(No));
                ApplyFilter();
            }
        }

        //private void UpdateIndexes()
        //{
        //    for (int i = 0; i < FilteredStudents.Count; i++)
        //    {
        //        FilteredStudents[i].Index = i + 1;
        //    }
        //}


        //private bool _isNumericUpDownEnabled = true;
        //public bool IsNumericUpDownEnabled
        //{
        //    get => _isNumericUpDownEnabled;
        //    set
        //    {
        //        if (_isNumericUpDownEnabled != value)
        //        {
        //            _isNumericUpDownEnabled = value;
        //            OnPropertyChanged(nameof(IsNumericUpDownEnabled));
        //        }
        //    }
        //}
        //private void UpdateNumericUpDownState()
        //{
        //    if (SelectedScoreType == "Điểm cuối kỳ")
        //    {
        //        IsNumericUpDownEnabled = false;
        //    }
        //    else
        //    {
        //        IsNumericUpDownEnabled = true;
        //    }
        //}


        //private int numberOfScoreColumns = 1;
        //public int NumberOfScoreColumns
        //{
        //    get => numberOfScoreColumns;
        //    set
        //    {
        //        // Lưu giá trị cũ để so sánh
        //        int oldValue = numberOfScoreColumns;

        //        // Nếu giá trị không đổi thì không làm gì
        //        if (value == oldValue) return;

        //        // --- LOGIC GIẢM SỐ CỘT ---
        //        if (value < oldValue)
        //        {
        //            // Vị trí (index) của cột điểm sắp bị xóa
        //            int columnIndexToRemove = value;

        //            // Tìm các học sinh đã có điểm ở cột này
        //            var studentsWithScore = FilteredStudents
        //                .Where(s => s.Scores.Count > columnIndexToRemove && s.Scores[columnIndexToRemove].HasValue)
        //                .ToList();

        //            if (studentsWithScore.Any())
        //            {
        //                // Nếu có học sinh đã nhập điểm, hiển thị thông báo và không cho giảm
        //                var studentNames = string.Join(", ", studentsWithScore.Select(s => s.Name));
        //                MessageBox.Show($"Không thể giảm số lần điểm. Các học sinh sau đã có điểm ở cột này: {studentNames}.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);

        //                // Trả lại giá trị cũ cho NumericUpDown trên UI
        //                OnPropertyChanged(nameof(NumberOfScoreColumns)); // Báo cho UI rằng giá trị đã bị "khôi phục"
        //                return; // Dừng thực thi
        //            }
        //        }

        //        // Nếu điều kiện giảm được thỏa mãn, hoặc nếu là tăng, thì tiến hành cập nhật
        //        {
        //            if (numberOfScoreColumns != value)
        //            {
        //                numberOfScoreColumns = value;
        //                OnPropertyChanged(nameof(NumberOfScoreColumns));
        //            }
        //        }

        //        // Cập nhật lại kích thước của các collection Scores
        //        foreach (var student in FilteredStudents)
        //        {
        //            // Tăng: Thêm các giá trị null
        //            while (student.Scores.Count < numberOfScoreColumns)
        //            {
        //                student.Scores.Add(null);
        //            }
        //            // Giảm: Xóa các phần tử cuối
        //            while (student.Scores.Count > numberOfScoreColumns)
        //            {
        //                student.Scores.RemoveAt(student.Scores.Count - 1);
        //            }
        //        }


        //    }
        //}

        //#endregion

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ViewGradeTableCommand { get; }


        public GradeViewModel(NavigationService navigationService, SchoolYearStore schoolYearStore)
        {
            _navigationService = navigationService;
            _schoolYearStore = schoolYearStore;
            _semesterList = new ObservableCollection<Hocky>();
            _subjectList = new ObservableCollection<Monhoc>();
            _gradeList = new ObservableCollection<Khoi>();
            _classList = new List<Lop>();
            _filteredClassList = new ObservableCollection<Lop>();
            _scoreTypes = new ObservableCollection<Loaidiem>();
            _no = 1;
            SaveCommand = new AsyncRelayCommand(SaveChanges);
            DeleteCommand = new AsyncRelayCommand(Delete);
            ViewGradeTableCommand = new AsyncRelayCommand(ViewGradeTable);
            Task.Run(LoadDataAsync).ConfigureAwait(false);
        }

        private async Task LoadDataAsync()
        {
            using (var context = new QuanlyhocsinhContext())
            {
                List<Monhoc> subjects = await context.Monhocs.ToListAsync();
                SubjectList = new ObservableCollection<Monhoc>(subjects);
                List<Hocky> semesters = await context.Hockies.ToListAsync();
                SemesterList = new ObservableCollection<Hocky>(semesters);
                List<Khoi> grades = await context.Khois.ToListAsync();
                GradeList = new ObservableCollection<Khoi>(grades);

                _classList = await context.Lops
                                                    .Where(l => l.Manamhoc == _schoolYearStore.SelectedSchoolYear.Manamhoc)
                                                    .ToListAsync();
                List<Loaidiem> scoreTypes = await context.Loaidiems.ToListAsync();
                ScoreTypeList = new ObservableCollection<Loaidiem>(scoreTypes);
                List<Thamso> thamsos = await context.Thamsos.ToListAsync();
                Thamso maximumScore = thamsos.FirstOrDefault(t => t.Tenthamso == "DiemToiDa");
                Thamso minimumScore = thamsos.FirstOrDefault(t => t.Tenthamso == "DiemToiThieu");
                StudentScoreDisplay.MaximumScore = maximumScore.Giatri ?? 10d;
                StudentScoreDisplay.MinimumScore = minimumScore.Giatri ?? 0d;
                //List<Hocsinh> students = await context.Hocsinhs
                //                                        .Include(hs => hs.CtBangdiemmonhocs)
                //                                        .ToListAsync();
                //ObservableCollection<StudentScoreDisplay> studentScoreDisplays = new ObservableCollection<StudentScoreDisplay>();
                //foreach(Hocsinh student in students)
                //{
                //    if(student.CtBangdiemmonhocs.Count > 0)
                //        studentScoreDisplays.Add(new StudentScoreDisplay(student, student.CtBangdiemmonhocs.FirstOrDefault().Dtbmon));
                //    else
                //        studentScoreDisplays.Add(new StudentScoreDisplay(student, -1d));


                //}
                //FilteredStudents = studentScoreDisplays;

            }
        }

        private void UpdateClassList()
        {
            if (SelectedGrade == null)
            {
                FilteredClassList = new ObservableCollection<Lop>();
            }
            else
            {
                FilteredClassList = new ObservableCollection<Lop>(_classList.Where(l => l.Makhoi == _selectedGrade.Makhoi));
                    
            }

            SelectedClass = FilteredClassList.FirstOrDefault();
        }


        private async Task ApplyFilter()
        {
            try
            {
                if (SelectedClass == null ||
                SelectedSemester == null ||
                SelectedSubject == null ||
                SelectedScoreType == null)
                {
                    return;
                }
                using (var context = new QuanlyhocsinhContext())
                {

                    Lop @class = await context.Lops
                                            .Include(l => l.Bangdiemmonhocs.Where(bd => bd.Mahocky == _selectedSemester.Mahocky && bd.Mamh == _selectedSubject.Mamh))
                                            .FirstOrDefaultAsync(l => l.Malop == _selectedClass.Malop);
                    ObservableCollection<StudentScoreDisplay> studentScoreDisplays = new ObservableCollection<StudentScoreDisplay>();
                    if (@class.Bangdiemmonhocs.Count > 0)
                    {
                        string BdId = @class.Bangdiemmonhocs.FirstOrDefault().Mabdmh;
                        List<Hocsinh> students = await context.Hocsinhs
                                                       .Where(hs => hs.CtLops.Any(ct => ct.Malop == _selectedClass.Malop))
                                                       .Include(hs => hs.CtBangdiemmonhocs.Where(ct => ct.Mabdmh == BdId))
                                                       .ThenInclude(ct => ct.CtDiemmonhocs.Where(ctd => ctd.Lan == _no && ctd.Maloaidiem == _selectedScoreType.Maloaidiem))
                                                       .ToListAsync();
                        foreach (Hocsinh student in students)
                        {
                            if (student.CtBangdiemmonhocs.Count > 0 && student.CtBangdiemmonhocs.FirstOrDefault().CtDiemmonhocs.Count > 0)
                            {
                                CtDiemmonhoc ctD = student.CtBangdiemmonhocs.FirstOrDefault().CtDiemmonhocs.FirstOrDefault();

                                studentScoreDisplays.Add(new StudentScoreDisplay(student, ctD.Diem));
                            }
                            else
                            {
                                studentScoreDisplays.Add(new StudentScoreDisplay(student, -1));
                            }
                        }
                    }
                    else
                    {
                        List<Hocsinh> students = await context.Hocsinhs
                                                      .Where(hs => hs.CtLops.Any(ct => ct.Malop == _selectedClass.Malop))
                                                      .ToListAsync();
                        foreach (Hocsinh student in students)
                        {
                            studentScoreDisplays.Add(new StudentScoreDisplay(student, -1));

                        }
                    }


                    FilteredStudents = studentScoreDisplays;
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

        private async Task SaveChanges()
        {
            try
            {
                if (SelectedClass == null ||
                SelectedSemester == null ||
                SelectedSubject == null ||
                SelectedScoreType == null)
                    throw new Exception("Xin hãy chọn đầy đủ thông tin!");

                using(var context = new QuanlyhocsinhContext())
                {
                    Lop @class = await context.Lops
                                           .Include(l => l.Bangdiemmonhocs.Where(bd => bd.Mahocky == _selectedSemester.Mahocky && bd.Mamh == _selectedSubject.Mamh))
                                           .ThenInclude(bd => bd.CtBangdiemmonhocs)
                                           .ThenInclude(ct => ct.CtDiemmonhocs)
                                           .FirstOrDefaultAsync(l => l.Malop == _selectedClass.Malop);
                    if(@class.Bangdiemmonhocs.Count > 0)
                    {
                        Bangdiemmonhoc bangdiemmonhoc = @class.Bangdiemmonhocs.FirstOrDefault();

                        foreach (StudentScoreDisplay studentScoreDisplay in _filteredStudents)
                        {
                            if (studentScoreDisplay.Score != -1)
                            {
                                CtBangdiemmonhoc ctBangdiemmonhoc = bangdiemmonhoc.CtBangdiemmonhocs.FirstOrDefault(ct => ct.Mahs == studentScoreDisplay.ID);
                                if (ctBangdiemmonhoc == null)
                                {
                                    ctBangdiemmonhoc = new CtBangdiemmonhoc(GenerateIdService.GenerateId(), bangdiemmonhoc.Mabdmh, studentScoreDisplay.ID, 0);
                                    context.CtBangdiemmonhocs.Add(ctBangdiemmonhoc);
                                }
                                CtDiemmonhoc ctDiemmonhoc = ctBangdiemmonhoc.CtDiemmonhocs.FirstOrDefault(ctd => ctd.Lan == _no && ctd.Maloaidiem == _selectedScoreType.Maloaidiem);
                                if(ctDiemmonhoc != null)
                                {
                                    ctDiemmonhoc.Diem = studentScoreDisplay.Score;
                                }
                                else
                                {
                                    CtDiemmonhoc newCtDiemmonhoc = new CtDiemmonhoc(ctBangdiemmonhoc.Mactbdmh, _selectedScoreType.Maloaidiem, _no, studentScoreDisplay.Score);
                                    ctBangdiemmonhoc.CtDiemmonhocs.Add(newCtDiemmonhoc);
                                }

                            }
                            else
                            {
                                CtDiemmonhoc ctDiemmonhoc = bangdiemmonhoc.CtBangdiemmonhocs.FirstOrDefault(ct => ct.Mahs == studentScoreDisplay.ID).CtDiemmonhocs.FirstOrDefault(ctd => ctd.Lan == _no && ctd.Maloaidiem == _selectedScoreType.Maloaidiem);
                                if ( ctDiemmonhoc != null)
                                {
                                    context.CtDiemmonhocs.Remove(ctDiemmonhoc);
                                }
                            }

                        }

                    }
                    else
                    {
                        Bangdiemmonhoc bangdiemmonhoc = new Bangdiemmonhoc(GenerateIdService.GenerateId(), _selectedClass.Malop, _selectedSubject.Mamh, _selectedSemester.Mahocky);
                        foreach(StudentScoreDisplay studentScoreDisplay in _filteredStudents)
                        {
                            CtBangdiemmonhoc ctBangdiemmonhoc = new CtBangdiemmonhoc(GenerateIdService.GenerateId(), bangdiemmonhoc.Mabdmh, studentScoreDisplay.ID, 0);
                            //bangdiemmonhoc.CtBangdiemmonhocs.Add(ctBangdiemmonhoc);
                            context.CtBangdiemmonhocs.Add(ctBangdiemmonhoc);

                            if (studentScoreDisplay.Score != -1)
                            {
                                CtDiemmonhoc ctDiemmonhoc = new CtDiemmonhoc(ctBangdiemmonhoc.Mactbdmh, _selectedScoreType.Maloaidiem, _no, studentScoreDisplay.Score);
                                //ctBangdiemmonhoc.CtDiemmonhocs.Add(ctDiemmonhoc);
                                context.CtDiemmonhocs.Add(ctDiemmonhoc);
                            }
                            
                        }
                        context.Bangdiemmonhocs.Add(bangdiemmonhoc);

                    }
                    await context.SaveChangesAsync();
                    MessageBox.Show("Lưu thành công");
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
        private async Task Delete()
        {
            try
            {
                if (SelectedClass == null ||
                SelectedSemester == null ||
                SelectedSubject == null ||
                SelectedScoreType == null)
                    throw new Exception("Xin hãy chọn đầy đủ thông tin!");

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa điểm?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new QuanlyhocsinhContext())
                    {
                        Lop @class = await context.Lops
                                               .Include(l => l.Bangdiemmonhocs.Where(bd => bd.Mahocky == _selectedSemester.Mahocky && bd.Mamh == _selectedSubject.Mamh))
                                               .ThenInclude(bd => bd.CtBangdiemmonhocs)
                                               .ThenInclude(ct => ct.CtDiemmonhocs)
                                               .FirstOrDefaultAsync(l => l.Malop == _selectedClass.Malop);
                        if (@class.Bangdiemmonhocs.Count > 0)
                        {
                            Bangdiemmonhoc bangdiemmonhoc = @class.Bangdiemmonhocs.FirstOrDefault();

                            foreach (StudentScoreDisplay studentScoreDisplay in _filteredStudents)
                            {

                                CtDiemmonhoc ctDiemmonhoc = bangdiemmonhoc.CtBangdiemmonhocs.FirstOrDefault(ct => ct.Mahs == studentScoreDisplay.ID).CtDiemmonhocs.FirstOrDefault(ctd => ctd.Lan == _no && ctd.Maloaidiem == _selectedScoreType.Maloaidiem);

                                if (ctDiemmonhoc != null)
                                {
                                    studentScoreDisplay.Score = -1;
                                    context.CtDiemmonhocs.Remove(ctDiemmonhoc);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("Không có dữ liệu để xóa!");
                        }

                        await context.SaveChangesAsync();
                        await ApplyFilter();

                        MessageBox.Show("Xóa thành công");
                    }
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

        private async Task ViewGradeTable()
        {
            try
            {
                if (SelectedClass == null ||
               SelectedSemester == null ||
               SelectedSubject == null ||
               SelectedScoreType == null)
                    throw new Exception("Xin hãy chọn đầy đủ thông tin!");

                using (var context = new QuanlyhocsinhContext())
                {
                    Bangdiemmonhoc bangdiemmonhoc = await context.Bangdiemmonhocs
                        .Where(bd => bd.Malop == _selectedClass.Malop && bd.Mahocky == _selectedSemester.Mahocky && bd.Mamh == _selectedSubject.Mamh)
                        .Include(bd => bd.MalopNavigation)
                        .Include(bd => bd.MahockyNavigation)
                        .Include(bd => bd.MamhNavigation)
                        .FirstOrDefaultAsync();
                    if(bangdiemmonhoc == null)
                        throw new Exception("Chưa có dữ liệu bảng điểm!");

                    _navigationService.PushStack(this);
                    _navigationService.Navigate(new ViewGradeTableViewModel(_navigationService, _schoolYearStore, bangdiemmonhoc));

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

        public override string ToString()
        {
            return "Điểm";
        }


        //#region Helper Methods for Enum Conversion

        //private Semester ConvertToSemester(string semester)
        //{
        //    return semester == "Học kỳ 1" ? Semester.Semester1 : Semester.Semester2;
        //}

        //private SubjectType ConvertToSubjectType(string subject)
        //{
        //    switch (subject)
        //    {
        //        case "Toán": return SubjectType.Toan;
        //        case "Lý": return SubjectType.Ly;
        //        case "Hóa": return SubjectType.Hoa;
        //        case "Sinh": return SubjectType.Sinh;
        //        case "Sử": return SubjectType.Su;
        //        case "Địa": return SubjectType.Dia;
        //        case "Văn": return SubjectType.Van;
        //        case "Tiếng Anh": return SubjectType.Anh;
        //        case "Giáo dục công dân": return SubjectType.GDCD;
        //        default:
        //            // Trả về một giá trị mặc định hoặc ném ra lỗi nếu cần
        //            throw new ArgumentOutOfRangeException(nameof(subject), $"Môn học không hợp lệ: {subject}");
        //    }
        //}

        //private ScoreType ConvertToScoreType(string scoreType)
        //{
        //    switch (scoreType)
        //    {
        //        case "Điểm miệng": return ScoreType.Oral;
        //        case "Điểm 15 phút": return ScoreType.Test15Min;
        //        case "Điểm 1 tiết": return ScoreType.Test1Period;
        //        case "Điểm cuối kỳ": return ScoreType.Final;
        //        default:
        //            // Trả về một giá trị mặc định hoặc ném ra lỗi nếu cần
        //            throw new ArgumentOutOfRangeException(nameof(scoreType), $"Loại điểm không hợp lệ: {scoreType}");
        //    }
        //}

        //private void SaveChanges()
        //{
        //    try
        //    {
        //        var semesterEnum = ConvertToSemester(SelectedSemester);
        //        var subjectEnum = ConvertToSubjectType(SelectedSubject);
        //        var scoreTypeEnum = ConvertToScoreType(SelectedScoreType);

        //        foreach (var student in FilteredStudents)
        //        {
        //            if (student.Scores.Any(s => s.HasValue && (s < 0 || s > 10)))
        //            {
        //                MessageBox.Show(
        //                    $"Học sinh {student.Name} có điểm không hợp lệ (0-10).",
        //                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                return;
        //            }

        //            student.UpdateScores(semesterEnum, subjectEnum, scoreTypeEnum);
        //        }

        //        var message = "Đã lưu điểm thành công!\n\n";
        //        foreach (var student in FilteredStudents)
        //        {
        //            var scoresText = string.Join(" ", student.Scores.Select(s => s.HasValue ? s.Value.ToString() : "null"));
        //            message += $"{student.Name}: {scoresText}\n";
        //        }

        //        MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Đã xảy ra lỗi khi lưu điểm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

    }
}