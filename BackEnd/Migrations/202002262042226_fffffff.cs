namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fffffff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Checks", "SumPrice", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Checks", "SumPrice", c => c.Double(nullable: false));
        }
    }
}
