namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Count", c => c.Int());
            AlterColumn("dbo.Products", "Massa", c => c.Double());
            AlterColumn("dbo.Products", "IsNumurable", c => c.Boolean());
            AlterColumn("dbo.Products", "CameToTheStorage", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "CameToTheStorage", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "IsNumurable", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Products", "Massa", c => c.Double(nullable: false));
            AlterColumn("dbo.Products", "Count", c => c.Int(nullable: false));
        }
    }
}
