namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feeee1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        transactionType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TransactionTypes", t => t.transactionType_ID)
                .Index(t => t.transactionType_ID);
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ProductReports", "transaction_ID", c => c.Int());
            CreateIndex("dbo.ProductReports", "transaction_ID");
            AddForeignKey("dbo.ProductReports", "transaction_ID", "dbo.Transactions", "ID");
            DropColumn("dbo.ProductReports", "IsLeft");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductReports", "IsLeft", c => c.Boolean());
            DropForeignKey("dbo.Transactions", "transactionType_ID", "dbo.TransactionTypes");
            DropForeignKey("dbo.ProductReports", "transaction_ID", "dbo.Transactions");
            DropIndex("dbo.Transactions", new[] { "transactionType_ID" });
            DropIndex("dbo.ProductReports", new[] { "transaction_ID" });
            DropColumn("dbo.ProductReports", "transaction_ID");
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.Transactions");
        }
    }
}
