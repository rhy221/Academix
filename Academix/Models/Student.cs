using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Academix.Models
{
    [Table("HOCSINH")]
    public class Student
    {
        [Key]
        public string ID { get; }
        [Column("HOTEN")]
        public string Name { get; set; }
        [Column("GIOITINH")]
        public bool Gender { get; set; }
        [Column("NGAYSINH")]
        public DateTime DateOfBirth { get; set; }
        [Column("DIACHI")]
        public string Address { get; set; }
        [Column("EMAIL")]
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
