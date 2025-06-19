using Academix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.ViewModels
{
    class GradeTableItemViewModel:BaseViewModel
    {
        private Hocsinh _student;

        public string Id => _student.Mahs;
        
        public string Name => _student.Hoten;

        public List<double> OralScoresList { get; set; } = new List<double>();
        public List<double> ShortScoresList { get; set; } = new List<double>();
        public List<double> PeriodScoresList { get; set; } = new List<double>();
        public List<double> FinalScoresList { get; set; } = new List<double>();

        public String OralScores
        {
            get
            {
                string scores = "";
                if(OralScoresList != null)
                    foreach(double score in OralScoresList)
                    {
                        scores += score.ToString() + " ";
                    }
                return scores;
            }

        }

        public String ShortScores
        {
            get
            {
                string scores = "";
                if (ShortScoresList != null)
                    foreach (double score in ShortScoresList)
                    {
                        scores += score.ToString() + " ";
                    }
                return scores;
            }

        }

        public String PeriodScores
        {
            get
            {
                string scores = "";
                if (PeriodScoresList != null)
                    foreach (double score in PeriodScoresList)
                    {
                        scores += score.ToString() + " ";
                    }
                return scores;
            }

        }

        public String FinalScores
        {
            get
            {
                string scores = "";
                if (FinalScoresList != null)
                    foreach (double score in FinalScoresList)
                    {
                        scores += score.ToString() + " ";
                    }
                return scores;
            }

        }

        public double GPA { get; set; }

        public GradeTableItemViewModel(Hocsinh student)
        {
            _student = student;
        }
    }
}
