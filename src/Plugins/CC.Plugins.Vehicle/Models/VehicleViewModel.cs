using CC.Plugins.Core.Attributes;
using FluentValidation;
using FluentValidation.Attributes;
using SmartStore.Web.Framework.Modelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Vehicle.Models
{
    [Validator(typeof(VehicleViewModelValidator))]
    public class VehicleViewModel: EntityModelBase
    {
        //public int Id { get; set; }

        [FilterField(FilterOperator = "int32Equal")]
        public int tbVehicleId { get; set; }

        public string tbVehicleName { get; set; }

        [FilterField(FilterOperator = "int32Equal")]
        public int ProgramId { get; set; }

        public string ProgramName { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string Name { get; set; }

        public int StartYW { get; set; }

        public string StartWeek { get; set; }

        public int EndYW { get; set; }

        public string EndWeek { get; set; }

        public int NumberOfLevels { get; set; }

        public decimal SellUnitPrice { get; set; }

        public bool? Deleted { get; set; }     

        public int CreatedByUser { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int? UpdatedByUser { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }
       
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }

    public partial class VehicleViewModelValidator : AbstractValidator<VehicleViewModel>
    {
        public VehicleViewModelValidator()
        {           
            RuleFor(x => x.Name).NotNull();

            RuleFor(x => x.NumberOfLevels)
                .NotNull()
                .Must(x => decimal.TryParse(x.ToString(), out var val) && val > 0)
                .WithMessage("NumberOfLevels must be a number");

            RuleFor(x => x.SellUnitPrice).Must(x => decimal.TryParse(x.ToString(), out var val) && val > 0)
                .NotNull()
                .WithMessage("SellUnitPrice must be a number");

            RuleFor(x => x.StartWeek).NotEmpty();


            RuleFor(x => x.EndWeek).NotEmpty();
               
            



        }
    }
}