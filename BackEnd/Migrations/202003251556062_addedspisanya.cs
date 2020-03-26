namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedspisanya : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpisannyaLena",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SpisannyaLesia",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Prihods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Rozhods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Spisannya",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SpisannyaOnAnotherMarket",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Sum = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpisannyaOnAnotherMarket");
            DropTable("dbo.Spisannya");
            DropTable("dbo.Rozhods");
            DropTable("dbo.Prihods");
            DropTable("dbo.SpisannyaLesia");
            DropTable("dbo.SpisannyaLena");
        }
    }
}
