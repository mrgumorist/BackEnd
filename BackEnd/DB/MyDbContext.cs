
using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackEnd.DB
{
  
        public class ApiContext : DbContext
        {
            public ApiContext() : base("DbbContext")
            {

            }

            public DbSet<User> Users { get; set; }
            public DbSet<UsersType> UsersTypes { get; set; }
        }
    
}