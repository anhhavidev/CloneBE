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
     new Product { ProductId = 1, Name = "Xe đạp thể thao", stock = 10, Description = "Xe cho dân chuyên nghiệp", Price = 1200.50, LinkImagesPath = "img1.jpg", CategoryId = 1 },
     new Product { ProductId = 2, Name = "Xe đạp đường phố", stock = 5, Description = "Nhẹ nhàng, dễ đi", Price = 800.00, LinkImagesPath = "img2.jpg", CategoryId = 2 },
     new Product { ProductId = 3, Name = "Xe đạp leo núi", stock = 7, Description = "Dành cho địa hình gồ ghề", Price = 1500.75, LinkImagesPath = "img3.jpg", CategoryId = 1 },
     new Product { ProductId = 4, Name = "Xe đạp gấp", stock = 3, Description = "Tiện lợi, gấp gọn dễ dàng", Price = 950.00, LinkImagesPath = "img4.jpg", CategoryId = 2 },
     new Product { ProductId = 5, Name = "Xe đạp trẻ em", stock = 15, Description = "An toàn, thiết kế cho trẻ nhỏ", Price = 500.00, LinkImagesPath = "img5.jpg", CategoryId = 3 },
     new Product { ProductId = 6, Name = "Xe đạp đua", stock = 4, Description = "Tốc độ cao, dành cho đường đua", Price = 1800.00, LinkImagesPath = "img6.jpg", CategoryId = 1 },
     new Product { ProductId = 7, Name = "Xe đạp nữ", stock = 6, Description = "Thiết kế nhẹ nhàng, phù hợp cho nữ", Price = 750.00, LinkImagesPath = "img7.jpg", CategoryId = 2 },
     new Product { ProductId = 8, Name = "Xe đạp điện trẻ em", stock = 8, Description = "Xe đạp có hỗ trợ điện dành cho trẻ em", Price = 1200.00, LinkImagesPath = "img8.jpg", CategoryId = 3 },
     new Product { ProductId = 9, Name = "Xe đạp touring", stock = 2, Description = "Xe phù hợp cho du lịch đường dài", Price = 2000.00, LinkImagesPath = "img9.jpg", CategoryId = 1 },
     new Product { ProductId = 10, Name = "Xe đạp mini", stock = 9, Description = "Xe đạp nhỏ gọn, phù hợp cho học sinh", Price = 650.00, LinkImagesPath = "img10.jpg", CategoryId = 2 }
 );

        }
    }
}
