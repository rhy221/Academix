using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class Parameter
    {
        public string Name { get; set; } = null!;

        public float? Value { get; set; }


    }
}
