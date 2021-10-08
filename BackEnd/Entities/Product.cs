using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("Products", Schema = "dbo")]
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string SpecialCode { get; set; }
        public long Uktzed { get; set; } = 0;
        public string Description { get; set; }
        public bool IsAkcis { get; set; } = false;
        public int? Count { get; set; } //Кількість або скільки грам
        public double? Massa { get; set; } //Кількість або скільки грам
        public bool? IsNumurable { get; set; } //1 - mass
        public double Price { get; set; } = 0; //Price by kg or 1
        public DateTime? CameToTheStorage { get; set; }
    }
}