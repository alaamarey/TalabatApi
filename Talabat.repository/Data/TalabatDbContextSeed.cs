using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.repository.Data
{
    public static class TalabatDbContextSeed
    {

        public static async Task SeedAsync(TalabatDbContext talabatDbContext)
        {
            if (!talabatDbContext.ProductBrands.Any())
            {
                var brandData = await File.ReadAllTextAsync("../Talabat.repository/Data/DataSeed/brands.json");
                List<ProductBrand> brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands.Count > 0)
                {
                    foreach (var brand in brands)
                        await talabatDbContext.AddAsync(brand);
                    await talabatDbContext.SaveChangesAsync();
                }
            }


            if (!talabatDbContext.ProductCategories.Any())
            {
                string CategoryData = await File.ReadAllTextAsync("../Talabat.repository/Data/DataSeed/categories.json");
                List<ProductCategory> categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);

                if (categories.Count > 0)
                {
                    foreach (var category in categories)
                        await talabatDbContext.AddAsync(category);
                    await talabatDbContext.SaveChangesAsync();
                }
            }


            if (!talabatDbContext.Products.Any())
            {
                string productData = await File.ReadAllTextAsync("../Talabat.repository/Data/DataSeed/products.json");
                List<Product> products = JsonSerializer.Deserialize<List<Product>>(productData);
                if (products.Count > 0)
                {
                    foreach (var product in products)
                        await talabatDbContext.AddAsync(product);
                    await talabatDbContext.SaveChangesAsync();
                }

            }


            if (!talabatDbContext.DeliveryMethods.Any())
            {
                string DeliveryData = File.ReadAllText("../Talabat.repository/Data/DataSeed/delivery.json");
                List<DeliveryMethod> deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

                if (deliveryMethods.Count > 0)
                {
                    foreach (var method in deliveryMethods)
                    {
                        await talabatDbContext.AddAsync(method);
                    }

                    await talabatDbContext.SaveChangesAsync();
                }

            }



        }
    }
}
