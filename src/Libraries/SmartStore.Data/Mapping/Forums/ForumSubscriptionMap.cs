using System.Data.Entity.ModelConfiguration;
using SmartStore.Core.Domain.Forums;

namespace SmartStore.Data.Mapping.Forums
{
    public partial class ForumSubscriptionMap : EntityTypeConfiguration<ForumSubscription>
    {
        public ForumSubscriptionMap()
        {
            this.ToTable("SSForums_Subscription");
            this.HasKey(fs => fs.Id);

            this.HasRequired(fs => fs.Customer)
                .WithMany()
                .HasForeignKey(fs => fs.CustomerId)
                .WillCascadeOnDelete(false);
        }
    }
}
