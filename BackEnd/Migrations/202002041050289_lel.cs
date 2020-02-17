namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.ProductReports", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "Sum", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Sum");
            DropColumn("dbo.ProductReports", "Price");
            DropColumn("dbo.Products", "Price");
        }
    }
}
