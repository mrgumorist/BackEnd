﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BackEnd.Entities
{
    [Table("StaticValuebles", Schema = "dbo")]
    public class StaticValueble
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

    }
}