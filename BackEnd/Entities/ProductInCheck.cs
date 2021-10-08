using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("ProductsInChecks", Schema = "dbo")]
    public class ProductInCheck
    {
        [Key]
        public int ID { get; set; }
        public int? IDOfProduct { get; set; }
        public string Name { get; set; }
        public string SpecialCode { get; set; }
        public string Description { get; set; }
        public bool IsAkcis { get; set; } = false;
        public long Uktzed { get; set; } = 0;
        public int? Count { get; set; } = 0; 
        public double? Massa { get; set; } = 0; 
        public bool? IsNumurable { get; set; } 
        public double Price { get; set; } 
        public virtual Check Check { get; set; }
    }
}