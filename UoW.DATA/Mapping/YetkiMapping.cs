using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class YetkiMapping : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasKey(x => x.ID);
            //builder.Property(X => X.ID).UseIdentityColumn();
            builder.Property(x => x.RolAdi).IsRequired().HasMaxLength(20);
        }
    }
}
