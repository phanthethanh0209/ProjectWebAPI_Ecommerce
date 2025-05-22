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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

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

            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Role");
                e.HasKey(pk => pk.Id);

                e.HasData(
                    new Role { Id = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), Name = "Admin" },
                    new Role { Id = Guid.Parse("a6f25e26-400e-4e55-97b3-94ac35fd32ee"), Name = "Customer" },
                    new Role { Id = Guid.Parse("fcd2fb03-484d-4c67-9940-bf4668619e9d"), Name = "Manager" });
            });

            modelBuilder.Entity<UserRole>(e =>
            {
                e.ToTable("UserRole");
                e.HasKey(pk => new { pk.UserId, pk.RoleId });

                e.HasOne(e => e.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.RoleId);
                e.HasOne(e => e.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.UserId);

                e.HasData(
                    new UserRole { UserId = Guid.Parse("d1407a13-ad60-48ad-8346-8fba9cbb4f41"), RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf") });
            });

            modelBuilder.Entity<Permission>(e =>
            {
                e.ToTable("Permission");
                e.HasKey(pk => pk.Id);

                e.HasData(
                    // User Permissions
                    new Permission { Id = Guid.Parse("d57fc2b6-0dc6-4a88-bbaf-bb876aa14310"), Name = "View.User", Description = "View user information" },
                    new Permission { Id = Guid.Parse("6610746a-389f-472e-b4eb-5eef6295b361"), Name = "Update.User", Description = "Update user information" },
                    new Permission { Id = Guid.Parse("ccaaebbd-4740-4516-b860-f6440fd4c55f"), Name = "Delete.User", Description = "Delete user" },
                    // Cart Permissions
                    new Permission { Id = Guid.Parse("238ec90a-8408-4fbe-a991-eb8f2eb4a28c"), Name = "View.Cart", Description = "View cart" },
                    new Permission { Id = Guid.Parse("b248903f-6891-4ebb-8b33-55a3dfa879df"), Name = "Update.Cart", Description = "Update cart" },
                    new Permission { Id = Guid.Parse("8c711aad-83b0-4e9e-af6d-b406ca1b150d"), Name = "Delete.Cart", Description = "Delete cart" },
                    // Order Permissions
                    new Permission { Id = Guid.Parse("22a895e7-40ac-4168-bc64-def6c6e945a6"), Name = "View.Order", Description = "View order" },
                    new Permission { Id = Guid.Parse("b764954a-903a-4ec2-ac11-988e2f9f22ae"), Name = "Create.Order", Description = "Create order" },
                    new Permission { Id = Guid.Parse("02cd1655-929b-437b-98e5-8a6551eae34d"), Name = "Update.Order", Description = "Update order" },
                    new Permission { Id = Guid.Parse("c5c65040-fd76-440f-bec0-51f86f19e431"), Name = "Delete.Order", Description = "Delete order" },
                    new Permission { Id = Guid.Parse("b84f9ad1-b64f-40b7-8fb2-f71c44ef0106"), Name = "Approve.Order", Description = "Approve order" },
                    // OrderItem Permissions
                    new Permission { Id = Guid.Parse("b898cc6c-762e-40d4-9a9c-0cd1a3eb7b8e"), Name = "View.OrderItem", Description = "View order item" },
                    new Permission { Id = Guid.Parse("b4c24c83-5628-4a9f-834b-10b07147481d"), Name = "Update.OrderItem", Description = "Update order item" },
                    // Product Permissions
                    new Permission { Id = Guid.Parse("e95657da-49a6-49fc-8d6f-384a2dc45cce"), Name = "View.Product", Description = "View product" },
                    new Permission { Id = Guid.Parse("7b61a427-65a7-459f-a0ed-41154bfaadf5"), Name = "Create.Product", Description = "Create product" },
                    new Permission { Id = Guid.Parse("a3ac9aaa-f16a-4100-b07b-e0d4897193db"), Name = "Update.Product", Description = "Update product" },
                    new Permission { Id = Guid.Parse("61d6359f-ce94-4d20-b2d7-66d03ff92891"), Name = "Delete.Product", Description = "Delete product" },
                    // Category Permissions
                    new Permission { Id = Guid.Parse("86685765-75fd-4f6d-9f05-46c65ada32d8"), Name = "View.Category", Description = "View category" },
                    new Permission { Id = Guid.Parse("e1532412-29c1-4e1c-afbf-0f5c5c4e41df"), Name = "Create.Category", Description = "Create category" },
                    new Permission { Id = Guid.Parse("00cff030-2921-4267-b802-86dc6d88174a"), Name = "Update.Category", Description = "Update category" },
                    new Permission { Id = Guid.Parse("3759092e-5e7b-4ca2-8c83-33104f549a0a"), Name = "Delete.Category", Description = "Delete category" },
                    // Payment Permissions
                    new Permission { Id = Guid.Parse("fa8fed50-24a7-4871-84d7-20b2376e8e27"), Name = "View.Payment", Description = "View payment" },
                    new Permission { Id = Guid.Parse("aeb40204-4004-4884-b510-e46742046698"), Name = "Create.Payment", Description = "Create payment" },
                    new Permission { Id = Guid.Parse("fcadd358-388c-4d20-a5af-86a235cf4352"), Name = "Update.Payment", Description = "Update payment" }
                );
            });

            modelBuilder.Entity<RolePermission>(e =>
            {
                e.ToTable("RolePermission");
                e.HasKey(pk => new { pk.RoleId, pk.PermissionId });

                e.HasOne(e => e.Role)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(r => r.RoleId);
                e.HasOne(e => e.Permission)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(r => r.PermissionId);

                e.HasData(
                    // Admin Permissions
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("d57fc2b6-0dc6-4a88-bbaf-bb876aa14310") }, // View.User
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("6610746a-389f-472e-b4eb-5eef6295b361") }, // Update.User
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("ccaaebbd-4740-4516-b860-f6440fd4c55f") }, // Delete.User
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("238ec90a-8408-4fbe-a991-eb8f2eb4a28c") }, // View.Cart
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("b248903f-6891-4ebb-8b33-55a3dfa879df") }, // Update.Cart
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("8c711aad-83b0-4e9e-af6d-b406ca1b150d") }, // Delete.Cart
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("22a895e7-40ac-4168-bc64-def6c6e945a6") }, // View.Order
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("b764954a-903a-4ec2-ac11-988e2f9f22ae") }, // Create.Order
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("02cd1655-929b-437b-98e5-8a6551eae34d") }, // Update.Order
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("c5c65040-fd76-440f-bec0-51f86f19e431") }, // Delete.Order
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("b84f9ad1-b64f-40b7-8fb2-f71c44ef0106") }, // Approve.Order
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("b898cc6c-762e-40d4-9a9c-0cd1a3eb7b8e") }, // View.OrderItem
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("b4c24c83-5628-4a9f-834b-10b07147481d") }, // Update.OrderItem
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("e95657da-49a6-49fc-8d6f-384a2dc45cce") }, // View.Product
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("7b61a427-65a7-459f-a0ed-41154bfaadf5") }, // Create.Product
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("a3ac9aaa-f16a-4100-b07b-e0d4897193db") }, // Update.Product
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("61d6359f-ce94-4d20-b2d7-66d03ff92891") }, // Delete.Product
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("86685765-75fd-4f6d-9f05-46c65ada32d8") }, // View.Category
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("e1532412-29c1-4e1c-afbf-0f5c5c4e41df") }, // Create.Category
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("00cff030-2921-4267-b802-86dc6d88174a") }, // Update.Category
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("3759092e-5e7b-4ca2-8c83-33104f549a0a") }, // Delete.Category
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("fa8fed50-24a7-4871-84d7-20b2376e8e27") }, // View.Payment
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("aeb40204-4004-4884-b510-e46742046698") }, // Create.Payment
                    new RolePermission { RoleId = Guid.Parse("4ea25da4-9081-41f8-83ba-2ba6e047fcbf"), PermissionId = Guid.Parse("fcadd358-388c-4d20-a5af-86a235cf4352") }, // Update.Payment
                                                                                                                                                                           // Customer Permissions
                    new RolePermission { RoleId = Guid.Parse("a6f25e26-400e-4e55-97b3-94ac35fd32ee"), PermissionId = Guid.Parse("22a895e7-40ac-4168-bc64-def6c6e945a6") }, // View.Order
                    new RolePermission { RoleId = Guid.Parse("a6f25e26-400e-4e55-97b3-94ac35fd32ee"), PermissionId = Guid.Parse("b764954a-903a-4ec2-ac11-988e2f9f22ae") }, // Create.Order
                    new RolePermission { RoleId = Guid.Parse("a6f25e26-400e-4e55-97b3-94ac35fd32ee"), PermissionId = Guid.Parse("238ec90a-8408-4fbe-a991-eb8f2eb4a28c") }, // View.Cart
                    new RolePermission { RoleId = Guid.Parse("a6f25e26-400e-4e55-97b3-94ac35fd32ee"), PermissionId = Guid.Parse("b248903f-6891-4ebb-8b33-55a3dfa879df") }, // Update.Cart
                                                                                                                                                                           // Manager Permissions
                    new RolePermission { RoleId = Guid.Parse("fcd2fb03-484d-4c67-9940-bf4668619e9d"), PermissionId = Guid.Parse("22a895e7-40ac-4168-bc64-def6c6e945a6") }, // View.Order
                    new RolePermission { RoleId = Guid.Parse("fcd2fb03-484d-4c67-9940-bf4668619e9d"), PermissionId = Guid.Parse("02cd1655-929b-437b-98e5-8a6551eae34d") }, // Update.Order
                    new RolePermission { RoleId = Guid.Parse("fcd2fb03-484d-4c67-9940-bf4668619e9d"), PermissionId = Guid.Parse("b84f9ad1-b64f-40b7-8fb2-f71c44ef0106") }, // Approve.Order
                    new RolePermission { RoleId = Guid.Parse("fcd2fb03-484d-4c67-9940-bf4668619e9d"), PermissionId = Guid.Parse("e95657da-49a6-49fc-8d6f-384a2dc45cce") } // View.Product
                );
            });

            modelBuilder.Entity<Cart>(e =>
            {
                e.ToTable("Cart");

                e.HasOne(e => e.User)
                    .WithOne(u => u.Cart)
                    .HasForeignKey<Cart>(fk => fk.UserId); // Cart dùng UserId làm FK

                e.HasData(
                    new Cart { Id = Guid.Parse("560a88f4-f3e5-40e6-8076-5cd6780dc14a"), TotalAmount = 0, UserId = Guid.Parse("d1407a13-ad60-48ad-8346-8fba9cbb4f41"), CreatedAt = new DateTime(2024, 03, 22, 12, 0, 0) });
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
                e.Property(x => x.Status).HasConversion<int>();

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
                e.Property(x => x.PaymentStatus).HasConversion<int>();

                e.HasOne(e => e.Order)
                    .WithOne(e => e.Payment)
                    .HasForeignKey<Payment>(e => e.OrderId);
            });
        }
    }
}
