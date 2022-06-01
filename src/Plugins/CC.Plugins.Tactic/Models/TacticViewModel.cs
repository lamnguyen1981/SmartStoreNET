using CC.Plugins.Core.Attributes;
using CC.Plugins.Core.Domain;
using FluentValidation;
using FluentValidation.Attributes;
using SmartStore.Web.Framework.Modelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Tactic.Models
{
    [Validator(typeof(TacticViewModelValidator))]
    public class TacticViewModel: EntityModelBase
    {
        //public int Id { get; set; }

        [FilterField(FilterOperator = "int32Equal")]
        public int tbTacticId { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string tbTacticName { get; set; }

        [FilterField(FilterOperator = "int32Equal")]
        public int VehicleId { get; set; }

        
        public string VehicleName { get; set; }

        public int StartYW { get; set; }

        public string StartWeek { get; set; }

        public string EndWeek { get; set; }

        public int EndYW { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string TacticCode { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string TacticType { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string TacticDescription { get; set; }

        public decimal TacticAmount { get; set; }

        public bool? Deleted { get; set; }

        public CCVehicle CCVehicle { get; set; }

        public int CreatedByUser { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int? UpdatedByUser { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }       

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }

    public partial class TacticViewModelValidator : AbstractValidator<TacticViewModel>
    {
        public TacticViewModelValidator()
        {
            RuleFor(x => x.TacticCode).NotNull();
            RuleFor(x => x.TacticDescription).NotNull();
            RuleFor(x => x.TacticType).NotNull();
            RuleFor(x => x.TacticAmount).NotNull();

            RuleFor(x => x.TacticAmount).Must(x => decimal.TryParse(x.ToString(), out var val) && val > 0)
                .WithMessage("TacticAmount must be a number");

            RuleFor(x => x.StartWeek).NotEmpty();

            RuleFor(x => x.EndWeek).NotEmpty();


        }
    }
}