using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data.Configurations.OrderConfig
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.SuTotal).HasColumnType("decimal(18,2)");

            builder.Property(o => o.Status)
                .HasConversion(os => os.ToString(), os => (OrderStatus)Enum.Parse(typeof(OrderStatus), os));

            builder.OwnsOne(o => o.ShippingAddress, sa => sa.WithOwner());

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
