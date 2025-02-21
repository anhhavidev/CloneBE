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
    public class Databasese : IdentityDbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Category>categories { get; set; }
        public Databasese(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CateConfig());
            builder.ApplyConfiguration(new ProductConfig());
            builder.Seed();
            base.OnModelCreating(builder);
           
        }
    }
}
