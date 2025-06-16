using Academix.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academix.Models.Enum;
using System.Collections.ObjectModel;

namespace Academix.Models
{
    public class StudentScoreDisplay : BaseViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged(nameof(Index));
            }
        }

        public List<SemesterScore> AllSemesterScores { get; set; } = new();

        private ObservableCollection<double?> scores = new();
        public ObservableCollection<double?> Scores
        {
            get => scores;
            set
            {
                if (scores != value)
                {
                    scores = value;
                    OnPropertyChanged(nameof(Scores));
                }
            }
        }

        public List<double> GetScores(Semester semester, SubjectType subject, ScoreType scoreType)
        {
            var semesterScore = AllSemesterScores.FirstOrDefault(s => s.Semester == semester);
            var subjectScore = semesterScore?.Subjects.FirstOrDefault(s => s.Subject == subject);
            var scoreGroup = subjectScore?.ScoreGroups.FirstOrDefault(g => g.ScoreType == scoreType);

            return scoreGroup?.Scores.Select(s => s.Value).ToList() ?? new List<double>();
        }

        /// <summary>
        /// [MỚI] Cập nhật điểm từ giao diện vào cấu trúc dữ liệu gốc (AllSemesterScores).
        /// </summary>
        /// <param name="semester">Học kỳ đang chọn</param>
        /// <param name="subject">Môn học đang chọn</param>
        /// <param name="scoreType">Loại điểm đang chọn</param>
        public void UpdateScores(Semester semester, SubjectType subject, ScoreType scoreType)
        {
            // BƯỚC 1: TÌM HOẶC TẠO HỌC KỲ
            var semesterScore = AllSemesterScores.FirstOrDefault(s => s.Semester == semester);
            if (semesterScore == null)
            {
                semesterScore = new SemesterScore { Semester = semester, Subjects = new List<SubjectScore>() };
                AllSemesterScores.Add(semesterScore);
            }

            // BƯỚC 2: TÌM HOẶC TẠO MÔN HỌC TRONG HỌC KỲ
            var subjectScore = semesterScore.Subjects.FirstOrDefault(s => s.Subject == subject);
            if (subjectScore == null)
            {
                subjectScore = new SubjectScore { Subject = subject, ScoreGroups = new List<ScoreGroup>() };
                semesterScore.Subjects.Add(subjectScore);
            }

            // BƯỚC 3: TÌM HOẶC TẠO NHÓM ĐIỂM TRONG MÔN HỌC
            var scoreGroup = subjectScore.ScoreGroups.FirstOrDefault(g => g.ScoreType == scoreType);
            if (scoreGroup == null)
            {
                scoreGroup = new ScoreGroup { ScoreType = scoreType, Scores = new List<Score>() };
                subjectScore.ScoreGroups.Add(scoreGroup);
            }

            // BƯỚC 4: CẬP NHẬT ĐIỂM
            // Xóa hết điểm cũ trong nhóm điểm này
            scoreGroup.Scores.Clear();

            // Lấy danh sách điểm mới từ giao diện (thuộc tính 'Scores' của lớp này)
            // và thêm chúng vào cấu trúc dữ liệu gốc.
            foreach (var scoreValue in this.Scores) // 'this.Scores' là ObservableCollection<double?>
            {
                // Chỉ lưu những ô đã được nhập điểm (không phải null)
                if (scoreValue.HasValue)
                {
                    scoreGroup.Scores.Add(new Score { Value = scoreValue.Value });
                }
            }
        }
    }
}