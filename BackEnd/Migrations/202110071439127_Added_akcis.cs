namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_akcis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsAkcis", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.ProductsInChecks", "IsAkcis", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Products", "IsAkcis");
            DropColumn("dbo.ProductsInChecks", "IsAkcis");
        }
    }
}
