using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;

namespace Talabat.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext _context)
        {
            // Brand Data
            if (_context.ProductBrands.Count() == 0)
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                    {
                        _context.Set<ProductBrand>().Add(brand);
                    }
                    await _context.SaveChangesAsync();
                }
            }

            // =====================================================================
            // =====================================================================
            // =====================================================================

            // Category data

            if (_context.ProductCategories.Count() == 0)
            {
                var CategoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");

                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);

                if (categories?.Count() > 0)
                {
                    foreach (var category in categories)
                    {
                        _context.Set<ProductCategory>().Add(category);
                    }
                    await _context.SaveChangesAsync();
                }
            }

            // =====================================================================
            // =====================================================================
            // =====================================================================

            //product data

            if (_context.products.Count() == 0)
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        _context.Set<Product>().Add(product);
                    }
                    await _context.SaveChangesAsync();
                }
            }


            // =====================================================================
            // =====================================================================
            // =====================================================================

            // delivery data

            if (_context.DeliveryMethods.Count() == 0)
            {
                var deliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");

                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                if (deliveryMethods?.Count() > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        _context.DeliveryMethods.Add(deliveryMethod);
                    }
                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}
