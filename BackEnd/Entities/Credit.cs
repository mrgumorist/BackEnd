using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("Creditors", Schema = "dbo")]
    public class Credit
    {
        [Key]
        public int ID { get; set; }
        public double Sum{ get; set; } = 0;
        public string Initsials { get; set; }
        public DateTime dateOfGetCredit { get; set; }
    }
}