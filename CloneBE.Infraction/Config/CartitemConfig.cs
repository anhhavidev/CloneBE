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
    public class CartitemConfig : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            //builder.HasKey(x => new {x.Productid ,x.CartId});
            builder.HasKey(x => x.CartitemId);
            builder.Property(x=>x.CartitemId).UseIdentityColumn();
            builder.HasOne(x => x.Cart).WithMany(x => x.cartItems).HasForeignKey(x => x.CartId);
            builder.HasOne(x => x.Product).WithMany(x => x.cartItems).HasForeignKey(x => x.Productid);
        }
    }
}
