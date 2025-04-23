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
    class Student
    {
        public string ID { get; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public Student(string id , string name , bool gender, DateTime dateOfBirth, string address, string email)
        {
            ID = id;
            Name = name;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Address = address;
            Email = email;
        }

        public Student(Student other)
        {
            ID = other.ID;
            Name = other.Name;
            Gender = other.Gender;
            DateOfBirth = other.DateOfBirth;
            Address = other.Address;
            Email = other.Email;
        }
        

        
    }
}
