//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universal_Task.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Employee_Table
    {
        public int EmpId { get; set; }
        public string UserName { get; set; }

        public string Address { get; set; }

        public string CityName { get; set; }

        public string State { get; set; }

        public string DoB { get; set; }

        public string UniqueCode { get; set; }

        public string Date { get; set; }

        public byte[] Image { get; set; }

        public string Password { get; set; }

        public Nullable<int> StateCode { get; set; }

        public Nullable<int> CityCode { get; set; }
    }
}
