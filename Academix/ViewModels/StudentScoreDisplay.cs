using Academix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Academix.Models;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace Academix.ViewModels
{
    public class StudentScoreDisplay : BaseViewModel
    {
        public static double MinimumScore = 0;
        public static double MaximumScore = 10;
        private Hocsinh _student;
        public string ID => _student.Mahs;
        public string Name => _student.Hoten;
        private double _score;
        public double Score
        {
            get => _score;
            set
            {
                if(value >= MinimumScore && value <= MaximumScore)
                {
                    _score = value;
                }
             
                OnPropertyChanged(nameof(Score));
            }
        }

        public StudentScoreDisplay(Hocsinh student, double score)
        {
            _student = student;
            _score = score;
        }




        //public List<SemesterScore> AllSemesterScores { get; set; } = new();

        //private ObservableCollection<double?> scores = new();
        //public ObservableCollection<double?> Scores
        //{
        //    get => scores;
        //    set
        //    {
        //        if (scores != value)
        //        {
        //            scores = value;
        //            OnPropertyChanged(nameof(Scores));
        //        }
        //    }
        //}

        //public List<double> GetScores(Semester semester, SubjectType subject, ScoreType scoreType)
        //{
        //    var semesterScore = AllSemesterScores.FirstOrDefault(s => s.Semester == semester);
        //    var subjectScore = semesterScore?.Subjects.FirstOrDefault(s => s.Subject == subject);
        //    var scoreGroup = subjectScore?.ScoreGroups.FirstOrDefault(g => g.ScoreType == scoreType);

        //    return scoreGroup?.Scores.Select(s => s.Value).ToList() ?? new List<double>();
        //}

        ///// <summary>
        ///// [MỚI] Cập nhật điểm từ giao diện vào cấu trúc dữ liệu gốc (AllSemesterScores).
        ///// </summary>
        ///// <param name="semester">Học kỳ đang chọn</param>
        ///// <param name="subject">Môn học đang chọn</param>
        ///// <param name="scoreType">Loại điểm đang chọn</param>
        //public void UpdateScores(Semester semester, SubjectType subject, ScoreType scoreType)
        //{
        //    // BƯỚC 1: TÌM HOẶC TẠO HỌC KỲ
        //    var semesterScore = AllSemesterScores.FirstOrDefault(s => s.Semester == semester);
        //    if (semesterScore == null)
        //    {
        //        semesterScore = new SemesterScore { Semester = semester, Subjects = new List<SubjectScore>() };
        //        AllSemesterScores.Add(semesterScore);
        //    }

        //    // BƯỚC 2: TÌM HOẶC TẠO MÔN HỌC TRONG HỌC KỲ
        //    var subjectScore = semesterScore.Subjects.FirstOrDefault(s => s.Subject == subject);
        //    if (subjectScore == null)
        //    {
        //        subjectScore = new SubjectScore { Subject = subject, ScoreGroups = new List<ScoreGroup>() };
        //        semesterScore.Subjects.Add(subjectScore);
        //    }

        //    // BƯỚC 3: TÌM HOẶC TẠO NHÓM ĐIỂM TRONG MÔN HỌC
        //    var scoreGroup = subjectScore.ScoreGroups.FirstOrDefault(g => g.ScoreType == scoreType);
        //    if (scoreGroup == null)
        //    {
        //        scoreGroup = new ScoreGroup { ScoreType = scoreType, Scores = new List<Score>() };
        //        subjectScore.ScoreGroups.Add(scoreGroup);
        //    }

        //    // BƯỚC 4: CẬP NHẬT ĐIỂM
        //    // Xóa hết điểm cũ trong nhóm điểm này
        //    scoreGroup.Scores.Clear();

        //    // Lấy danh sách điểm mới từ giao diện (thuộc tính 'Scores' của lớp này)
        //    // và thêm chúng vào cấu trúc dữ liệu gốc.
        //    foreach (var scoreValue in this.Scores) // 'this.Scores' là ObservableCollection<double?>
        //    {
        //        // Chỉ lưu những ô đã được nhập điểm (không phải null)
        //        if (scoreValue.HasValue)
        //        {
        //            scoreGroup.Scores.Add(new Score { Value = scoreValue.Value });
        //        }
        //    }
        //}
    }
}