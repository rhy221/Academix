using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    [Table("NAMHOC")]
    class SchoolYear
    {
        [Key]
        public String _id;
        [Column("NAM1")]
        public int FirstYear { get; private set; }
        [Column("NAM2")]
        public int SecondYear { get; private set; }

        public SchoolYear(String id,int firstYear, int secondYear)
        {
            _id = id;
            FirstYear = firstYear;
            SecondYear = secondYear;
        }

        public override string ToString()
        {
            return $"{FirstYear}-{SecondYear}";
        }
    }
}
