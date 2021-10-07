namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fefe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StaticValuebles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StaticValuebles");
        }
    }
}
