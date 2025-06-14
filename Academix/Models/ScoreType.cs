using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class ScoreType
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Multiplier { get; set; }

        public virtual ICollection<CtDiemmonhoc> CtDiemmonhocs { get; set; } = new List<CtDiemmonhoc>();

        public virtual ICollection<SubjectDetail> SubjectDetails { get; set; } = new List<SubjectDetail>();

    }
}
