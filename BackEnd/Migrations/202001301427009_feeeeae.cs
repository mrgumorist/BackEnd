﻿namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feeeeae : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ProductReports", "IDOfProduct", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductReports", "IDOfProduct");
            DropTable("dbo.Logs");
        }
    }
}
