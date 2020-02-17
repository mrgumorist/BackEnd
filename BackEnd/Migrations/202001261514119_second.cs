namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SpecialCode = c.String(),
                        Description = c.String(),
                        Count = c.Int(nullable: false),
                        Massa = c.Double(nullable: false),
                        IsNumurable = c.Boolean(nullable: false),
                        CameToTheStorage = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductReports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SpecialCode = c.String(),
                        Description = c.String(),
                        Count = c.Int(nullable: false),
                        Massa = c.Double(nullable: false),
                        IsNumurable = c.Boolean(nullable: false),
                        DateOfIt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductReports");
            DropTable("dbo.Products");
        }
    }
}
