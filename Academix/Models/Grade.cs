using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    [Table("KHOI")]
    class Grade
    {
       [Column("MAKHOI")]
       public String Id { get; private set; }
       [Column("TENKHOI")]
       public String Name { get; private set; }
        
        public Grade(String id, String name)
        {
            Id = id;
            Name = name;
        }
        
    }
}
