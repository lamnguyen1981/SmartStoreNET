namespace SmartStore.CreditCardPay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_Initializing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CCCustomerPaymentAddess",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 100),
                        State = c.String(nullable: false, maxLength: 50),
                        Zip = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CCCustomerProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CCCustomerPaymentAddess", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CCCustomerPayment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerProfileId = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CCCustomerProfile", t => t.CustomerProfileId, cascadeDelete: true)
                .Index(t => t.CustomerProfileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CCCustomerPayment", "CustomerProfileId", "dbo.CCCustomerProfile");
            DropForeignKey("dbo.CCCustomerProfile", "Id", "dbo.CCCustomerPaymentAddess");
            DropIndex("dbo.CCCustomerPayment", new[] { "CustomerProfileId" });
            DropIndex("dbo.CCCustomerProfile", new[] { "Id" });
            DropTable("dbo.CCCustomerPayment");
            DropTable("dbo.CCCustomerProfile");
            DropTable("dbo.CCCustomerPaymentAddess");
        }
    }
}
