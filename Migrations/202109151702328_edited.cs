namespace EcommerceApi_dotNetFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edited : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaystackTransactions", "AuthorizationDetails_Id", "dbo.PaystackBankAuthorizations");
            DropIndex("dbo.PaystackTransactions", new[] { "AuthorizationDetails_Id" });
            DropColumn("dbo.PaystackTransactions", "AuthorizationDetails_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaystackTransactions", "AuthorizationDetails_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.PaystackTransactions", "AuthorizationDetails_Id");
            AddForeignKey("dbo.PaystackTransactions", "AuthorizationDetails_Id", "dbo.PaystackBankAuthorizations", "Id");
        }
    }
}
