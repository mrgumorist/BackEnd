namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Surname", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Initials");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Initials", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Surname");
        }
    }
}
