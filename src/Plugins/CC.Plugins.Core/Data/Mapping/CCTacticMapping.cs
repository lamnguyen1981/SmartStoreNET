using CC.Plugins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Data.Mapping
{
    public class CCTacticMapping : EntityTypeConfiguration<CCTactic>
    {
        public CCTacticMapping()
        { 
            this.ToTable("CCTactic");
                this.HasKey(x => x.Id);             
                this.Property(x => x.TacticCode).IsRequired().HasMaxLength(50);
            this.Property(x => x.TacticType).IsRequired().HasMaxLength(50);
                this.Property(x => x.TacticDescription).IsRequired().HasMaxLength(150);
                this.Property(x => x.TacticAmount).IsRequired();
                this.Property(x => x.EndYW).IsRequired();
                this.Property(x => x.StartYW).IsRequired();                           
                this.Property(x => x.Deleted).IsOptional();
                this.Property(x => x.UpdatedByUser).IsOptional();
                this.Property(x => x.UpdatedOnUtc).IsOptional();
            HasRequired(p => p.CCVehicle)
              .WithMany()
              .HasForeignKey(p => p.VehicleId)
              .WillCascadeOnDelete(false);


        }
    }
}