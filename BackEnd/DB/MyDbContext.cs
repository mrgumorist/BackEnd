
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
        public DbSet<Product> ProductsAvaliale { get; set; }
        public DbSet<ProductReport> Reports { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<ProductInCheck> ProductsInCheck{ get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Credit> Creditors { get; set; }
        public DbSet<CalcIn> Prihods { get; set; }
        public DbSet<CalcOut> Rozhods { get; set; }
        public DbSet<Spisannya> Spisannya { get; set; }
        public DbSet<SpisannyaOnAnotherMarket> SpisannyaOnAnotherMarket { get; set; }
        public DbSet<Lesia> LesiaS { get; set; }
        public DbSet<Lena> LenaS { get; set; }
        public DbSet<StaticValueble> StaticValuebles { get; set; }
    }
    
    
}