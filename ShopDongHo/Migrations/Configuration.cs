using ShopDongHo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ShopDongHo.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ShopDongHo.Models.Entities.ShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShopDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var categories = new List<Category>
            {
                new Category { Name = "Đồng hồ nam", Description = "Đồng hồ dành cho nam giới", CreatedDate = DateTime.Now },
                new Category { Name = "Đồng hồ nữ", Description = "Đồng hồ dành cho nữ giới", CreatedDate = DateTime.Now },
                new Category { Name = "Đồng hồ thể thao", Description = "Đồng hồ thể thao chống nước", CreatedDate = DateTime.Now },
                new Category { Name = "Đồng hồ thông minh", Description = "Smartwatch và đồng hồ thông minh", CreatedDate = DateTime.Now }
            };

            categories.ForEach(c => context.Categories.AddOrUpdate(cat => cat.Name, c));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Casio MTP-VD01D-1BVUDF",
                    CategoryId = 1,
                    Brand = "Casio",
                    Price = 1250000,
                    OriginalPrice = 1500000,
                    Stock = 50,
                    Description = "Đồng hồ nam Casio dây da, mặt tròn, phong cách cổ điển",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Citizen NH8350-83L",
                    CategoryId = 1,
                    Brand = "Citizen",
                    Price = 4800000,
                    OriginalPrice = 5500000,
                    Stock = 30,
                    Description = "Đồng hồ nam Citizen automatic, dây thép không gỉ",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Seiko 5 SNKL23K1",
                    CategoryId = 1,
                    Brand = "Seiko",
                    Price = 3200000,
                    Stock = 25,
                    Description = "Đồng hồ nam Seiko automatic, dây thép",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Orient FAC00009N0",
                    CategoryId = 1,
                    Brand = "Orient",
                    Price = 4500000,
                    OriginalPrice = 5200000,
                    Stock = 20,
                    Description = "Đồng hồ nam Orient automatic, mặt số xanh",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Casio LTP-V005D-2BUDF",
                    CategoryId = 2,
                    Brand = "Casio",
                    Price = 980000,
                    OriginalPrice = 1200000,
                    Stock = 40,
                    Description = "Đồng hồ nữ Casio dây da, mặt xanh nhỏ gọn",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Citizen EW2470-87F",
                    CategoryId = 2,
                    Brand = "Citizen",
                    Price = 5200000,
                    Stock = 15,
                    Description = "Đồng hồ nữ Citizen Eco-Drive, dây thép vàng",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Seiko SUR634P1",
                    CategoryId = 2,
                    Brand = "Seiko",
                    Price = 3800000,
                    OriginalPrice = 4200000,
                    Stock = 20,
                    Description = "Đồng hồ nữ Seiko quartz, mặt số đính đá",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "G-Shock GA-2100-1A1DR",
                    CategoryId = 3,
                    Brand = "Casio",
                    Price = 3100000,
                    Stock = 35,
                    Description = "Đồng hồ G-Shock chống nước 200m, thiết kế octagon",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "G-Shock DW-5600E-1VDF",
                    CategoryId = 3,
                    Brand = "Casio",
                    Price = 2200000,
                    OriginalPrice = 2500000,
                    Stock = 45,
                    Description = "Đồng hồ G-Shock classic, chống sốc, chống nước",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Citizen Eco-Drive BN0150-28E",
                    CategoryId = 3,
                    Brand = "Citizen",
                    Price = 8500000,
                    Stock = 10,
                    Description = "Đồng hồ lặn Citizen Eco-Drive, chống nước 300m",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Apple Watch Series 9",
                    CategoryId = 4,
                    Brand = "Apple",
                    Price = 10500000,
                    OriginalPrice = 12000000,
                    Stock = 25,
                    Description = "Smartwatch Apple Watch Series 9, GPS, 45mm",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Product
                {
                    Name = "Samsung Galaxy Watch 6",
                    CategoryId = 4,
                    Brand = "Samsung",
                    Price = 7200000,
                    OriginalPrice = 8500000,
                    Stock = 30,
                    Description = "Smartwatch Samsung Galaxy Watch 6, theo dõi sức khỏe",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            };

            products.ForEach(p => context.Products.AddOrUpdate(prod => prod.Name, p));
            context.SaveChanges();

            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@shopdongho.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                FullName = "Administrator",
                Phone = "0123456789",
                Address = "123 Đường ABC, TP.HCM",
                Role = "Admin",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            var customerUser = new User
            {
                Username = "customer",
                Email = "customer@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("customer123"),
                FullName = "Khách Hàng Mẫu",
                Phone = "0987654321",
                Address = "456 Đường XYZ, TP.HCM",
                Role = "Customer",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            context.Users.AddOrUpdate(u => u.Username, adminUser, customerUser);
            context.SaveChanges();
        }
    }
}
