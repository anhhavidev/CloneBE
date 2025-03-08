using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using CloneBE.Infraction.Config;
using CloneBE.Infraction.Seed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloneBE.Infraction.Presistences
{
    public class Databasese : IdentityDbContext<AppUser,AppIdentityRole, string>
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Category>categories { get; set; }
        public DbSet<Cart>carts { get; set; }
        public DbSet<CartItem>cartItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        public Databasese(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CateConfig());
            builder.ApplyConfiguration(new ProductConfig());
            builder.ApplyConfiguration(new CartitemConfig());
            builder.ApplyConfiguration(new CartConfig());
            builder.Seed();
            base.OnModelCreating(builder);
            // Cấu hình kiểu dữ liệu Id là int
            builder.Entity<AppUser>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd(); // Tự động tăng

            builder.Entity<AppIdentityRole>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd(); // Tự động tăng

        }
    }
}
