using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CloneBE.Infraction.Seed
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var roleId = Guid.NewGuid().ToString();
            var adminId = Guid.NewGuid().ToString();

            modelBuilder.Entity<AppIdentityRole>().HasData(new AppIdentityRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId, // Id bây giờ là string
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "honganh@gmail.com",
                NormalizedEmail = "HONGANH@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abc1234@"),
                SecurityStamp = Guid.NewGuid().ToString(), // SecurityStamp cần là string
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Address = " g",  // Thêm trường này
                PhoneNumber = "0123456789",   // Nếu cũng là required
                FullName = "User"       // Nếu cũng là required

            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = adminId
            });
            // 6 Category laptop
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Ultrabooks", Description = "Lightweight and portable laptops" },
                new Category { CategoryId = 2, Name = "Gaming Laptops", Description = "High performance laptops for gaming" },
                new Category { CategoryId = 3, Name = "Business Laptops", Description = "Laptops designed for business use" },
                new Category { CategoryId = 4, Name = "2-in-1 Laptops", Description = "Convertible laptops with touchscreen" },
                new Category { CategoryId = 5, Name = "Budget Laptops", Description = "Affordable laptops for everyday use" },
                new Category { CategoryId = 6, Name = "Workstations", Description = "Powerful laptops for professional work" }



                );
            // 60 Product laptops (10 product mỗi category)
            var products = new List<Product>();
            int productId = 1;
            var rnd = new Random();

            for (int categoryId = 1; categoryId <= 6; categoryId++)
            {
                string categoryName = categoryId switch
                {
                    1 => "Ultrabook",
                    2 => "Gaming Laptop",
                    3 => "Business Laptop",
                    4 => "2-in-1 Laptop",
                    5 => "Budget Laptop",
                    6 => "Workstation",
                    _ => "Laptop"
                };

                string categoryImageFolder = categoryId switch
                {
                    1 => "ultrabook",
                    2 => "gaming_laptop",
                    3 => "business_laptop",
                    4 => "2in1_laptop",
                    5 => "budget_laptop",
                    6 => "workstation",
                    _ => "laptop"
                };

                for (int i = 1; i <= 10; i++)
                {
                    products.Add(new Product
                    {
                        ProductId = productId++,
                        Name = $"{categoryName} Model {i}",
                        Description = $"High quality {categoryName} with excellent features, model {i}.",
                        stock = rnd.Next(5, 100),
                        Price = Math.Round(rnd.NextDouble() * (3000 - 500) + 500, 2),
                        LinkImagesPath = $"images/{categoryImageFolder}_model_{i}.jpg",
                        CategoryId = categoryId
                    });
                }
            }
            modelBuilder.Entity<Product>().HasData(products.ToArray());


        }
    }
}

