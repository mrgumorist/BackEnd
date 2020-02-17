namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checks", "DateCreatingOfCheck", c => c.DateTime(nullable: false));
            AddColumn("dbo.Checks", "DateCloseOfCheck", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Checks", "DateCloseOfCheck");
            DropColumn("dbo.Checks", "DateCreatingOfCheck");
        }
    }
}
