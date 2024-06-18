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
    public class Property
    {
        [Key]
        public int ID { get; set; }

        public string? propCustodian { get; set; } //added

        public DateOnly? AssignDate { get; set; }
        public DateOnly? ReturnDate { get; set; }

        //MAIN Item Attributes
        public string propArticle { get; set; }                                                                                    //REQUIRED
        public string propDescription { get; set; }
        public string propPropertyNo { get; set; }                                                                              //REQUIRED

        [StringLength(10)]
        public string propUnitOfMeasure { get; set; }                                                                           //REQUIRED

        public double? propUnitValue { get; set; }                                                                               //REQUIRED
        public int? propBalancePerCardQty { get; set; }                                                                          //REQUIRED
        public int? propOnHandPerCardQty { get; set; }                                                                           //REQUIRED
        public int? propShortageOverageQty { get; set; }
        public double? propShortageOverageValue { get; set; }
        public DateOnly? propYrAcquired { get; set; }


        public string? propRemarks { get; set; }
        public string? propStatus { get; set; }
        public bool? IsRemoved { get; set; } = false;
        public string? category { get; set; }

    }
}