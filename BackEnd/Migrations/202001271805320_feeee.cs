namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feeee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductReports", "Count", c => c.Int());
            AlterColumn("dbo.ProductReports", "Massa", c => c.Double());
            AlterColumn("dbo.ProductReports", "IsNumurable", c => c.Boolean());
            AlterColumn("dbo.ProductReports", "DateOfIt", c => c.DateTime());
            AlterColumn("dbo.ProductReports", "IsLeft", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductReports", "IsLeft", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProductReports", "DateOfIt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductReports", "IsNumurable", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProductReports", "Massa", c => c.Double(nullable: false));
            AlterColumn("dbo.ProductReports", "Count", c => c.Int(nullable: false));
        }
    }
}
