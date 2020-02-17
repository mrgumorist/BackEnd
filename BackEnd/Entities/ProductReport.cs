using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("ProductReports", Schema = "dbo")]
    public class ProductReport
    {
        [Key]
        public int ID { get; set; }
        public int? IDOfProduct { get; set; }
        public string Name { get; set; }
        public string SpecialCode { get; set; }
        public string Description { get; set; }
        public int? Count { get; set; } = 0; //Кількість або скільки грам
        public double? Massa { get; set; } = 0; //Кількість або скільки грам
        public bool? IsNumurable { get; set; } //1 - mass
        public double Price { get; set; } //Price by kg or 1
        public DateTime? DateOfIt { get; set; }
        public virtual Transaction transaction { get; set; }
    }
}