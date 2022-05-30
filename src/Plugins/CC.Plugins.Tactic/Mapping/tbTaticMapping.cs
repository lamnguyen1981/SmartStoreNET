using CC.Plugins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CC.Plugins.Tactic.Mapping
{
    public class tbTaticMapping : EntityTypeConfiguration<tbTacticID>
    {
        public tbTaticMapping()
        { 
            this.ToTable("tbTacticID");
            this.Property(x => x.Id).HasColumnName("TacticID");
            //this.Property(x => x.Tactic).HasColumnName("TacticID");

        }
    }
}