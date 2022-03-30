namespace SmartStore.CreditCardPay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database_Initializing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CCCustomerPaymentProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerPaymentProfileId = c.String(nullable: false, maxLength: 100),
                        CustomerPaymentProfileAlias = c.String(maxLength: 100),
                        CreatedOnUtc = c.DateTime(),
                        CreatedByUser = c.Int(),
                        CCustomerProfileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CCCustomerProfile", t => t.CCustomerProfileId)
                .Index(t => t.CCustomerProfileId);
            
            CreateTable(
                "dbo.CCCustomerProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        HlCustomerProfileId = c.String(nullable: false, maxLength: 100),
                        CreatedOnUtc = c.DateTime(),
                        CreateByUser = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CCCustomerPaymentProfile", "CCustomerProfileId", "dbo.CCCustomerProfile");
            DropIndex("dbo.CCCustomerPaymentProfile", new[] { "CCustomerProfileId" });
            DropTable("dbo.CCCustomerProfile");
            DropTable("dbo.CCCustomerPaymentProfile");
        }
    }
}
