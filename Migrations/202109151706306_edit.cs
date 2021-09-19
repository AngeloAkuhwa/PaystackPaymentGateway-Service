namespace EcommerceApi_dotNetFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaystackTransactions", "ReferenceId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaystackTransactions", "ReferenceId");
        }
    }
}
