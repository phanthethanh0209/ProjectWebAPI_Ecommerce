using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data;

namespace ECommerce.Infrastructure.Seeders
{
    internal class ProductSeeder(ApplicationDbContext dbContext)
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Products.Any())
                {
                    IEnumerable<Product> products = GetProducts();
                }
            }
        }

        private IEnumerable<Product> GetProducts()
        {
            List<Product> products = [
                new Product
                {
                    Id = Guid.Parse("c107fe9b-ae3e-4b74-bb43-0d390c79d486"),
                    Name = "iPhone 14 Pro",
                    Description = "Điện thoại cao cấp với camera 48MP và chip A16 Bionic",
                    Price = 27990000,
                    StockQuantity = 50,
                    CategoryId = Guid.Parse("3156994b-be86-4e69-b0c3-b383ab203a12"),
                    CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0)
                },
                new Product
                {
                    Id = Guid.Parse("f19411a4-e9dd-41af-9773-73051ab930a5"),
                    Name = "Samsung S23 Ultra",
                    Description = "Smartphone flagship với bút S-Pen và camera 200MP",
                    Price = 30990000,
                    StockQuantity = 40,
                    CategoryId = Guid.Parse("3156994b-be86-4e69-b0c3-b383ab203a12"),
                    CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0)
                },
                new Product
                {
                    Id = Guid.Parse("4e3d18ea-474a-4a7d-96d9-89e6545d626b"),
                    Name = "Tai nghe AirPods",
                    Description = "Tai nghe không dây với chất lượng âm thanh tốt và chống ồn chủ động",
                    Price = 4990000,
                    StockQuantity = 100,
                    CategoryId = Guid.Parse("a02a5c5a-f789-4bfe-abff-064ded0a2cde"),
                    CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0)
                },
                new Product
                {
                    Id = Guid.Parse("a1034129-c1fd-42d5-9773-b878fde40e1a"),
                    Name = "Dell XPS 15",
                    Description = "Ultrabook cao cấp với màn hình 4K OLED và CPU Intel Core i9",
                    Price = 45990000,
                    StockQuantity = 25,
                    CategoryId = Guid.Parse("a9ea7cd2-f475-4a7c-9f25-7116f4073c64"),
                    CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0)
                }];

            return products;
        }
    }
}
