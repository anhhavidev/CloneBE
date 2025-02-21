using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using Microsoft.EntityFrameworkCore;

namespace CloneBE.Infraction.Seed
{
    public static  class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
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
