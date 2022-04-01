using FluentValidation;
using FluentValidation.Attributes;

namespace SmartStore.CreditCardPay.Models
{
    [Validator(typeof(CreditCardChargeDetailRequestValidator))]
    public class CreditCardChargeDetailRequest : HeartlandRequestBase
    {       

        public string OrderId { get; set; }         
           
        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public decimal WithConvenienceAmt { get; set; }

        public decimal WithShippingAmt { get; set; }

        public decimal WithSurchargeAmount { get; set; }
                     

    }

    public partial class CreditCardChargeDetailRequestValidator : AbstractValidator<CreditCardChargeDetailRequest>
    {
        public CreditCardChargeDetailRequestValidator()
        {
            RuleFor(x => x.Amount).NotNull()
                .GreaterThan(0)
                .Must(x => decimal.TryParse(x.ToString(), out var val) && val > 0)
                    .WithMessage("Invalid Number.");            
            RuleFor(x => x.Currency).NotNull();
        }
    }
}