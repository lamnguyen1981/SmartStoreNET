using CC.Plugins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Data.Mapping
{
    public class tbVehicleMapping : EntityTypeConfiguration<tbVehicleID>
    {
        public tbVehicleMapping()
        { 
            this.ToTable("tbVehicleID");
            this.Property(x => x.Id).HasColumnName("VehicleID");
            

        }
    }
}