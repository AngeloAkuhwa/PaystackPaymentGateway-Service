namespace EcommerceApi_dotNetFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentserviecCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaystackBankAuthorizations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ReferenceId = c.String(),
                        AuthorizationCode = c.String(),
                        LastFour = c.String(),
                        ExpiryMonth = c.String(),
                        ExpiryYear = c.String(),
                        CardType = c.String(),
                        Bank = c.String(),
                        CountryCode = c.String(),
                        Brand = c.String(),
                        AccountName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaystackTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AuthorizationDetails_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaystackBankAuthorizations", t => t.AuthorizationDetails_Id)
                .Index(t => t.AuthorizationDetails_Id);
            
            DropTable("dbo.Transactions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TRANSACTION_ID = c.Guid(nullable: false, identity: true),
                        DATE = c.DateTime(),
                        PAY_REQUEST_ID = c.String(),
                        AMOUNT = c.Int(nullable: false),
                        REFERENCE = c.String(),
                        TRANSACTION_STATUS = c.String(),
                        RESULT_CODE = c.Int(nullable: false),
                        RESULT_DESC = c.String(),
                        CUSTOMER_EMAIL_ADDRESS = c.String(),
                    })
                .PrimaryKey(t => t.TRANSACTION_ID);
            
            DropForeignKey("dbo.PaystackTransactions", "AuthorizationDetails_Id", "dbo.PaystackBankAuthorizations");
            DropIndex("dbo.PaystackTransactions", new[] { "AuthorizationDetails_Id" });
            DropTable("dbo.PaystackTransactions");
            DropTable("dbo.PaystackBankAuthorizations");
        }
    }
}
