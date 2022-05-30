using CC.Plugins.Core.Attributes;
using FluentValidation;
using FluentValidation.Attributes;
using SmartStore.Web.Framework.Modelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Program.Models
{
    [Validator(typeof(ProgramViewModelValidator))]
    public class ProgramViewModel: EntityModelBase
    {
        //public int Id { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string tbProgramCode { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string Code { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string Name { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string ShortDescription { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string LongDescription { get; set; }

        [FilterField(FilterOperator = "textLike")]
        public string ProgramType { get; set; }

        public string tbProgramName { get; set; }


        public int? Sequence { get; set; }


        public int? LockOutDays { get; set; }


        public int? Frequency { get; set; }


        public int? AppliedTo { get; set; }

        public int CreatedByUser { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int? UpdatedByUser { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }

        public bool? Deleted { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }

    public partial class ProgramViewModelValidator : AbstractValidator<ProgramViewModel>
    {
        public ProgramViewModelValidator()
        {
            RuleFor(x => x.Code).NotNull();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.ShortDescription).NotNull();
            RuleFor(x => x.LongDescription).NotNull();
            RuleFor(x => x.ProgramType).NotNull();
            RuleFor(x => x.AppliedTo).Must(x => decimal.TryParse(x.ToString(), out var val) && val > 0);

            RuleFor(x => x.Frequency).Must(x => decimal.TryParse(x.ToString(), out var val) && val > 0);

            RuleFor(x => x.LockOutDays).Must(x => decimal.TryParse(x.ToString(), out var val) && val > 0);

            RuleFor(x => x.Sequence).Must(x => decimal.TryParse(x.ToString(), out var val) && val > 0);
          
           
        }
    }
}