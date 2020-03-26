using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("SpisannyaOnAnotherMarket", Schema = "dbo")]
    public class SpisannyaOnAnotherMarket
    {
        [Key]
        public int ID { get; set; }
        public double Sum { get; set; }
    }
}