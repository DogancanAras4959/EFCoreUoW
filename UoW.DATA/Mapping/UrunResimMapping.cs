using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class UrunResimMapping : IEntityTypeConfiguration<UrunResimler>
    {
        public void Configure(EntityTypeBuilder<UrunResimler> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ResimYol).HasMaxLength(75);
            builder.HasOne(x => x.urun).WithMany(x => x.urunResimler).HasForeignKey(x => x.UrunID);
        }
    }
}
