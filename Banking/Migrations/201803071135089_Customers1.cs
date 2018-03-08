namespace Banking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customers1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TransactionHistories", newName: "Transactions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Transactions", newName: "TransactionHistories");
        }
    }
}
