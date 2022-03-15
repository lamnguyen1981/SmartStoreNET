namespace SmartStore.CreditCardPay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Address_FK_CustomerProfile : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CCCustomerProfile", new[] { "Id" });
            AddColumn("dbo.CCCustomerProfile", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.CCCustomerPaymentAddess", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CCCustomerPaymentAddess", new[] { "Id" });
            DropColumn("dbo.CCCustomerProfile", "AddressId");
            CreateIndex("dbo.CCCustomerProfile", "Id");
        }
    }
}
