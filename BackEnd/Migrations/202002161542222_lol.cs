namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SumPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductsInChecks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDOfProduct = c.Int(),
                        Name = c.String(),
                        SpecialCode = c.String(),
                        Description = c.String(),
                        Count = c.Int(),
                        Massa = c.Double(),
                        IsNumurable = c.Boolean(),
                        Price = c.Double(nullable: false),
                        Check_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Checks", t => t.Check_ID)
                .Index(t => t.Check_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsInChecks", "Check_ID", "dbo.Checks");
            DropIndex("dbo.ProductsInChecks", new[] { "Check_ID" });
            DropTable("dbo.ProductsInChecks");
            DropTable("dbo.Checks");
        }
    }
}
