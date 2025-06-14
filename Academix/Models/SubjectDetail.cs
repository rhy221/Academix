using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class SubjectDetail
    {
        public string SubjectId { get; set; } = null!;

        public string ScoreTypeId { get; set; } = null!;

        public virtual ScoreType ScoreTypeIdNavigation { get; set; } = null!;

        public virtual Subject SubjectIdNavigation { get; set; } = null!;
    }
}
