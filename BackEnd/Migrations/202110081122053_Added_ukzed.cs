namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ukzed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductsInChecks", "Uktzed", c => c.Long(nullable: false));
            AddColumn("dbo.Products", "Uktzed", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Uktzed");
            DropColumn("dbo.ProductsInChecks", "Uktzed");
        }
    }
}
