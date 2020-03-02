using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("Checks", Schema = "dbo")]
    public class Check
    {
        [Key]
        public int ID { get; set; }
        public ICollection<ProductInCheck> Products { get; set; }
        public double? SumPrice { get; set; } = 0;
        public DateTime DateCreatingOfCheck { get; set; }
        public DateTime DateCloseOfCheck { get; set; }
        public string TypeOfPay { get; set; } = "";
    }
}