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
    public class Supply
    {
        [Key]
        public int ID { get; set; }

        public DateOnly? IssueDate { get; set; }

        public string supStockLetter { get; set; }                                
        public string? supArticle { get; set; }                                    
        public int? supStockNumber { get; set; }                                   
        public string supDescription { get; set; }          
        

        public string supUnitType { get; set; }                                 
        public double? supAverageUnitCost { get; set; }
        public int? supDelivery { get; set; }


        public DateOnly supBegInvDate { get; set; } = new DateOnly();
        public int? supBeginningInventoryQty { get; set; }  = 0;
        public int? supEndingInventoryQty { get; set; } = 0;
        public int? supBalance { get; set; }
        public double? supTotalAmount { get; set; }
        public int? supIssuance { get; set; } = 0;


        public string? supRemark { get; set; } //added                           
        public bool? IsRemoved { get; set; } = false;
        public string? category { get; set; }

    }
}