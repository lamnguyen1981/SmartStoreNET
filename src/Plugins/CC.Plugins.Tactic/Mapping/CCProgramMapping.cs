using CC.Plugins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CC.Plugins.Tactic.Mapping
{
    public class CCTacticMapping : EntityTypeConfiguration<CCTactic>
    {
        public CCTacticMapping()
        { 
            this.ToTable("CCTactic");
                this.HasKey(x => x.Id);
                this.Property(x => x.Code).IsRequired().HasMaxLength(20);
                this.Property(x => x.tbTacticCode).IsRequired().HasMaxLength(50);
                this.Property(x => x.Name).IsRequired().HasMaxLength(100);
                this.Property(x => x.ShortDescription).IsRequired().HasMaxLength(250);
                this.Property(x => x.LongDescription).IsRequired();
                this.Property(x => x.TacticType).IsRequired().HasMaxLength(20);
                this.Property(x => x.Sequence).IsOptional();
                this.Property(x => x.LockOutDays).IsOptional();
                this.Property(x => x.Frequency).IsOptional();
                this.Property(x => x.AppliedTo).IsOptional();
                this.Property(x => x.Deleted).IsOptional();
                this.Property(x => x.UpdatedByUser).IsOptional();
                this.Property(x => x.UpdatedOnUtc).IsOptional();

        }
    }
}