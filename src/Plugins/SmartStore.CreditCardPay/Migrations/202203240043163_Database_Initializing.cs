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
                        CustomerProfileId = c.Int(nullable: false),
                        HlCustomerProfileId = c.String(maxLength: 100),
                        CustomerPaymentProfileId = c.String(nullable: false, maxLength: 100),
                        CustomerPaymentProfileAlias = c.String(maxLength: 100),
                        CreateDate = c.DateTime(),
                        CreateByUser = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CCCustomerPaymentProfile");
        }
    }
}
