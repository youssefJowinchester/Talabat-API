using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data.Configurations.OrderConfig
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(oi => oi.Product, p => p.WithOwner());

            builder.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
        }
    }
}
