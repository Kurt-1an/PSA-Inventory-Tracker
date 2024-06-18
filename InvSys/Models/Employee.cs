using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.CodeDom;

namespace InvSys.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        public string? empName { get; set; }
        public string? empPos { get; set; }
        [EmailAddress]
        public string? empMail { get; set; }
        public bool? IsRemoved { get; set; } = false;
    }
}
