using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("Spisannya", Schema = "dbo")]
    public class Spisannya
    {
        [Key]
        public int ID { get; set; }
        public double Sum { get; set; }
    }
}