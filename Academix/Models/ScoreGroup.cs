using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academix.Models.Enum;
using static System.Formats.Asn1.AsnWriter;

namespace Academix.Models
{
    public class ScoreGroup
    {
        public ScoreType ScoreType { get; set; }
        public List<Score> Scores { get; set; } = new();
    }
}
