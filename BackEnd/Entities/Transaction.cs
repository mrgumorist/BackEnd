using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("Transactions", Schema = "dbo")]
    public class Transaction
    {
        [Key]
        public int ID { get; set; }
        public DateTime date { get; set; }
        public virtual TransactionType transactionType { get; set; }
        public double Sum { get; set; } 
        public ICollection<ProductReport> Reports { get; set; }
    }
}