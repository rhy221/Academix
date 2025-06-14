using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Academix.Models
{
    public class Student
    {
        public string ID { get; set;  }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public byte[] Images { get; set; }

        public Student(string id , string name , bool gender, DateTime dateOfBirth, string address, string email, byte [] images)
        {
            ID = id;
            Name = name;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Address = address;
            Email = email;
            Images = images;
        }

        public Student(Student other)
        {
            ID = other.ID;
            Name = other.Name;
            Gender = other.Gender;
            DateOfBirth = other.DateOfBirth;
            Address = other.Address;
            Email = other.Email;
            Images = other.Images;
        }
        

        
    }
}
