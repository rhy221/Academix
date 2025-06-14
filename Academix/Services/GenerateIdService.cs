using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Services
{
    public class GenerateIdService
    {
        public static string GenerateId()
        {
            string datePart = DateTime.UtcNow.ToString("yyMMddHH"); 
            var rand = new Random();
            string randomPart = rand.Next(10, 99).ToString(); 
            return datePart + randomPart; 
        }
    }
}
