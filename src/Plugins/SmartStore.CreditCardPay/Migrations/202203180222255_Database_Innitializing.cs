namespace SmartStore.CreditCardPay.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Database_Innitializing : DbMigration
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CCCustomerProfile", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CCCustomerProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CCCustomerPayment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerProfileId = c.Int(nullable: false),
                        TransactionId = c.String(nullable: false),
                        HlCustomerProfileId = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CCCustomerPaymentAddess", "Id", "dbo.CCCustomerProfile");
            DropIndex("dbo.CCCustomerPaymentAddess", new[] { "Id" });
            DropTable("dbo.CCCustomerPayment");
            DropTable("dbo.CCCustomerProfile");
            DropTable("dbo.CCCustomerPaymentAddess");
        }
    }
}
