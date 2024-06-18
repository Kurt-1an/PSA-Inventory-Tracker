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
    public class SupplyHistory
    {
        [Key]
        public int ID { get; set; }


        // Other properties similar to Supply entity
        public int SupplyID { get; set; }
        public DateOnly DateModified { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}