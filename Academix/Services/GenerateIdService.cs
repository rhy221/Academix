using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Academix.Services
{
    public class GenerateIdService
    {
        public static string GenerateId()
        {
            //string datePart = DateTime.UtcNow.ToString("yyMMsfff");
            //int seed = Guid.NewGuid().GetHashCode();
            //var rand = new Random(seed);
            //string randomPart = rand.Next(10, 99).ToString(); 
            //return datePart + randomPart;
            //
            // 7 ký tự thời gian dựa trên ticks
            string timePart = (DateTime.UtcNow.Ticks % 10_000_000).ToString("D7");

            // 3 ký tự ngẫu nhiên
            var buffer = new byte[2]; // 2 bytes ~ 0-65535
            RandomNumberGenerator.Fill(buffer);
            int rand = BitConverter.ToUInt16(buffer, 0) % 1000; // 0-999
            string randomPart = rand.ToString("D3");

            return timePart + randomPart;
        }
    }
}
