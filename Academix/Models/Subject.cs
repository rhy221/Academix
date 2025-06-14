using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
    public class Subject
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Multiplier { get; set; }

        public virtual ICollection<Bangdiemmonhoc> Bangdiemmonhocs { get; set; } = new List<Bangdiemmonhoc>();

        public virtual ICollection<Bctongketmon> Bctongketmons { get; set; } = new List<Bctongketmon>();

        public virtual ICollection<ScoreType> ScoreTypes { get; set; } = new List<ScoreType>();

        public Subject(string id, string name, int multiplier)
        {
            Id = id;
            Name = name;
            Multiplier = multiplier;
        }

        public Subject(Subject other)
        {
            Id = other.Id;
            Name = other.Name;
            Multiplier = other.Multiplier;
        }

    }
}
