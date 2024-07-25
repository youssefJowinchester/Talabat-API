using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    }
}
