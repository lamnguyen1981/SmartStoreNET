using System.Data.Entity.ModelConfiguration;
using SmartStore.Rules.Domain;

namespace SmartStore.Data.Mapping.Rules
{
    public class RuleSetMap : EntityTypeConfiguration<RuleSetEntity>
    {
        public RuleSetMap()
        {
            this.ToTable("SSRuleSet");
        }
    }

    public class RuleMap : EntityTypeConfiguration<RuleEntity>
    {
        public RuleMap()
        {
            this.ToTable("SSRule");
        }
    }
}