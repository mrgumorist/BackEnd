namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ffffffff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Creditors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                        Initsials = c.String(),
                        dateOfGetCredit = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Creditors");
        }
    }
}
