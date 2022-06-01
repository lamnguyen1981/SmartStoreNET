using CC.Plugins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Data.Mapping
{
    public class CCVehicleMapping : EntityTypeConfiguration<CCVehicle>
    {
        public CCVehicleMapping()
        { 
            this.ToTable("CCVehicle");
                this.HasKey(x => x.Id);             
                this.Property(x => x.Name).IsRequired().HasMaxLength(50);
                this.Property(x => x.StartYW).IsRequired();
                this.Property(x => x.EndYW).IsRequired();
                this.Property(x => x.NumberOfLevels).IsRequired();
                this.Property(x => x.SellUnitPrice).IsRequired();             
                this.Property(x => x.Deleted).IsOptional();
                this.Property(x => x.UpdatedByUser).IsOptional();
                this.Property(x => x.UpdatedOnUtc).IsOptional();
            HasRequired(p => p.CCProgram)
              .WithMany()
              .HasForeignKey(p => p.ProgramId)
              .WillCascadeOnDelete(false);


        }
    }
}