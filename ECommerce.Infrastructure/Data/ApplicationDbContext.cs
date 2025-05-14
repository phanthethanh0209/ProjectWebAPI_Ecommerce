using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        #region DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("User");
                e.HasKey(pk => pk.Id);
                e.HasIndex(u => u.Email).IsUnique();

                // password = Thanh123@
                e.HasData(
                    new User { Id = Guid.Parse("d1407a13-ad60-48ad-8346-8fba9cbb4f41"), Email = "Thanh123@gmail.com", Name = "Thanh", Phone = "0985632147", Password = "$2a$11$0/CP8hh.odVCJCJi0d261ObBVpXQ06FuX53Aiq6Fn.0pKKdcdnMz2", CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0) });
            });

            modelBuilder.Entity<Cart>(e =>
            {
                e.ToTable("Cart");

                e.HasOne(e => e.User)
                    .WithOne(u => u.Cart)
                    .HasForeignKey<Cart>(fk => fk.UserId); // Cart dùng UserId làm FK
            });

            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Category");
                e.HasKey(pk => pk.Id);

                e.HasData(
                    new Category { Id = Guid.Parse("3156994b-be86-4e69-b0c3-b383ab203a12"), Name = "Điện thoại", Description = "Các loại điện thoại", CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0) },
                    new Category { Id = Guid.Parse("a9ea7cd2-f475-4a7c-9f25-7116f4073c64"), Name = "Laptop", Description = "Máy tính xách tay", CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0) },
                    new Category { Id = Guid.Parse("a02a5c5a-f789-4bfe-abff-064ded0a2cde"), Name = "Phụ kiện", Description = "Tai nghe, sạc, v.v.", CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0) },
                    new Category { Id = Guid.Parse("878dfce8-75e5-4a2b-ba2f-a05322e18239"), Name = "Đồ gia dụng", Description = "Máy lạnh, tủ lạnh", CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0) },
                    new Category { Id = Guid.Parse("296a460b-3216-4557-983c-58c58d87fdf7"), Name = "TV & Màn hình", Description = "Các loại tivi, màn hình", CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0) });
            });

            modelBuilder.Entity<RefreshToken>(e =>
            {
                e.ToTable("RefreshToken");
                e.HasKey(pk => pk.Id);

                e.HasOne(e => e.User)
                    .WithMany(t => t.RefreshTokens)
                    .HasForeignKey(fk => fk.UserId);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Product");
                e.HasKey(pk => pk.Id);
                e.Property(t => t.Price).HasColumnType("decimal(18,2)");

                e.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(fk => fk.CategoryId);


                e.HasData(
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
                    }
                );
            });


            modelBuilder.Entity<CartItem>(e =>
            {
                e.ToTable("CartItem");
                e.HasKey(pk => new { pk.CartId, pk.ProductId });

                e.HasOne(e => e.Cart)
                    .WithMany(t => t.CartItems)
                    .HasForeignKey(fk => fk.CartId);

                e.HasOne(e => e.Product)
                    .WithMany(t => t.CartItems)
                    .HasForeignKey(fk => fk.ProductId);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasKey(pk => pk.Id);
                e.Property(t => t.TotalAmount).HasColumnType("decimal(18,2)");

                e.HasOne(e => e.User)
                    .WithMany(t => t.Orders)
                    .HasForeignKey(fk => fk.UserId);
            });

            modelBuilder.Entity<OrderItem>(e =>
            {
                e.ToTable("OrderItem");
                e.HasKey(pk => new { pk.OrderId, pk.ProductId });
                e.Property(t => t.Price).HasColumnType("decimal(18,2)");

                e.HasOne(e => e.Order)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.OrderId);

                e.HasOne(e => e.Product)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.ProductId);
            });


            modelBuilder.Entity<Payment>(e =>
            {
                e.ToTable("Payment");
                e.HasKey(pk => pk.Id);

                e.HasOne(e => e.Order)
                    .WithOne(e => e.Payment)
                    .HasForeignKey<Payment>(e => e.OrderId);
            });
        }
    }
}
