namespace Banking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customers3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Credit", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "Debit", c => c.Double(nullable: false));
            DropColumn("dbo.Transactions", "Value");
            DropColumn("dbo.Transactions", "TransactionType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "TransactionType", c => c.String());
            AddColumn("dbo.Transactions", "Value", c => c.Double(nullable: false));
            DropColumn("dbo.Transactions", "Debit");
            DropColumn("dbo.Transactions", "Credit");
        }
    }
}
