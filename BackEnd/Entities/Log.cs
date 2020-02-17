using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("Logs", Schema = "dbo")]
    public class Log
    {
        [Key]
        public int ID { get; set; }
        public DateTime date{get;set;}
        public string Description { get; set; }
    }
}