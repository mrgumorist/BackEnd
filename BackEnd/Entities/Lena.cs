﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("SpisannyaLena", Schema = "dbo")]
    public class Lena
    {
        [Key]
        public int ID { get; set; }
        public double Sum { get; set; }
    }
}