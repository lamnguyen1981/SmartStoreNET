using System.Data.Entity.ModelConfiguration;
using SmartStore.Core.Domain.Catalog;

namespace SmartStore.Data.Mapping.Catalog
{
    public partial class RelatedProductMap : EntityTypeConfiguration<RelatedProduct>
    {
        public RelatedProductMap()
        {
            this.ToTable("SSRelatedProduct");
            this.HasKey(c => c.Id);
        }
    }
}