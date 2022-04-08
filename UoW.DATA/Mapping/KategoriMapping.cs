using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class KategoriMapping : IEntityTypeConfiguration<Kategori>
    {
        public void Configure(EntityTypeBuilder<Kategori> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.KategoriAdi).HasMaxLength(50).IsRequired();
            builder.Property(x => x.KategoriFoto).HasMaxLength(50).IsRequired();
            builder.Property(x => x.KategoriAciklama).HasMaxLength(300).IsRequired();
            builder.HasOne(x => x.kullanici).WithMany(x => x.kategoriler).HasForeignKey(x => x.YoneticiID);
        }
    }
}
