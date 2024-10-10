using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Universal_Task.Models
{
    public class Employee1
    {
        public int EmpId { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }     
        public string City { get; set; }
        public string State { get; set; }
        public DateTime? DoB { get; set; }
        public string UniqueCode { get; set; }
        public DateTime? Date { get; set; }
        public byte[] Image { get; set; } // Store the image as byte array
        public string Password { get; set; }
    }
}