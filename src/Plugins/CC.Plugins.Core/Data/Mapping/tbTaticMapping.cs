using CC.Plugins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CC.Plugins.Core.Data.Mapping
{
    public class tbTacticleMapping : EntityTypeConfiguration<tbTacticID>
    {
        public tbTacticleMapping()
        { 
            this.ToTable("tbTaticID");
            this.Property(x => x.Id).HasColumnName("TacticID");
            

        }
    }
}