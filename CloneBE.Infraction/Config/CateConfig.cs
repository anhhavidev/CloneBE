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
    public class CateConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.CategoryId);
            builder.Property(x=>x.CategoryId).UseIdentityColumn();
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
