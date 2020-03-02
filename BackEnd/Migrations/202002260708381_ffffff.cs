namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ffffff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checks", "TypeOfPay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Checks", "TypeOfPay");
        }
    }
}
