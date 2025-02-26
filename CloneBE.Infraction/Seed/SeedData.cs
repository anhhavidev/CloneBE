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
    public static  class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var roleId = Guid.NewGuid().ToString();
            var adminId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });

            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
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

            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = adminId
            });
            modelBuilder.Entity<Category>().HasData(
                 new Category { CategoryId = 1, Name = "Xe đạp địa hình", Description = "Dành cho đường núi" },
                 new Category { CategoryId = 2, Name = "Xe đạp đường phố", Description = "Dành cho đường bằng" },
                 new Category { CategoryId = 3, Name = "Xe đạp trẻ em", Description = "Dành cho trẻ em" }


                );
            modelBuilder.Entity<Product>().HasData(

                new Product { ProductId = 1, Name = "Xe đạp thể thao",Quanlity=10, Description = "Xe cho dân chuyên nghiệp", Price = 1200.50, linkimages = "img1.jpg", CategoryId = 1 },
                new Product { ProductId = 2, Name = "Xe đạp đường phố",Quanlity=5, Description = "Nhẹ nhàng, dễ đi", Price = 800.00, linkimages = "img2.jpg", CategoryId = 2 }
            );

        }
    }
}
