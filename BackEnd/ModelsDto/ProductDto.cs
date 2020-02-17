using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.ModelsDto
{
    public class ProductDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SpecialCode { get; set; }
        public string Description { get; set; }
        public int? Count { get; set; } //Кількість або скільки грам
        public double? Massa { get; set; } //Кількість або скільки грам
        public bool? IsNumurable { get; set; } //1 - mass
        public DateTime? CameToTheStorage { get; set; }
        public double Price { get; set; } //Price by kg or 1
    }
}