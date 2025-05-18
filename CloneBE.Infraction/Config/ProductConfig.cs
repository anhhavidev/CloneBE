using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloneBE.Domain.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBE.Infraction.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);
            builder.Property(x=>x.ProductId).UseIdentityColumn();
            builder.Property(x => x.LinkImagesPath).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            //quan he 
            builder.HasOne(x => x.category).WithMany(x => x.products).HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
