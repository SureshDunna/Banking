namespace Banking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountNumber = c.Long(nullable: false, identity: true),
                        AccountName = c.String(),
                        CustomerId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.AccountNumber)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        PostCode = c.Int(nullable: false),
                        EmailAddress = c.String(),
                        MobileNumber = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.TransactionHistories",
                c => new
                    {
                        TransactionId = c.Long(nullable: false, identity: true),
                        Value = c.Double(nullable: false),
                        TransactionType = c.String(),
                        AccountNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Accounts", t => t.AccountNumber, cascadeDelete: true)
                .Index(t => t.AccountNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionHistories", "AccountNumber", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "CustomerId", "dbo.Customers");
            DropIndex("dbo.TransactionHistories", new[] { "AccountNumber" });
            DropIndex("dbo.Accounts", new[] { "CustomerId" });
            DropTable("dbo.TransactionHistories");
            DropTable("dbo.Customers");
            DropTable("dbo.Accounts");
        }
    }
}
