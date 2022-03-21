namespace SmartStore.CreditCardPay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PaymentMethodType_Field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CCCustomerPayment", "PaymentMethodType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CCCustomerPayment", "PaymentMethodType");
        }
    }
}
