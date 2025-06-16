using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academix.Models.Enum;

namespace Academix.Models
{
    public class SubjectScore
    {
        public SubjectType Subject { get; set; }
        public List<ScoreGroup> ScoreGroups { get; set; } = new();
    }
}
