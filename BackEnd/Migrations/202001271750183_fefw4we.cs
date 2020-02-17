namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fefw4we : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductReports", "IsLeft", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductReports", "IsLeft");
        }
    }
}
