using CC.Plugins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CC.Plugins.Program.Mapping
{
    public class CCProgramMapping : EntityTypeConfiguration<CCProgram>
    {
        public CCProgramMapping()
        { 
            this.ToTable("CCProgram");
                this.HasKey(x => x.Id);
                this.Property(x => x.Code).IsRequired().HasMaxLength(20);
                this.Property(x => x.tbProgramCode).IsRequired().HasMaxLength(50);
                this.Property(x => x.Name).IsRequired().HasMaxLength(100);
                this.Property(x => x.ShortDescription).IsRequired().HasMaxLength(250);
                this.Property(x => x.LongDescription).IsRequired();
                this.Property(x => x.ProgramType).IsRequired().HasMaxLength(20);
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