using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Models
{
   public abstract class Report
    {
        public string ID { get; }

        protected Report(string id)
        {
            ID = id;
        }

       
    }
}
