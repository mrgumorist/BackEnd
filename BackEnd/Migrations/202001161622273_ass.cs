namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Initials = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        UsersType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.UsersTypes", t => t.UsersType_Id)
                .Index(t => t.UsersType_Id);
            
            CreateTable(
                "dbo.UsersTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UsersType_Id", "dbo.UsersTypes");
            DropIndex("dbo.Users", new[] { "UsersType_Id" });
            DropTable("dbo.UsersTypes");
            DropTable("dbo.Users");
        }
    }
}
