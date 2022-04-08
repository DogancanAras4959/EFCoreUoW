using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class HaberMapping : IEntityTypeConfiguration<Haber>
    {
        public void Configure(EntityTypeBuilder<Haber> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.HaberAdi).HasMaxLength(100).IsRequired();
            builder.Property(x => x.HaberSpot).HasMaxLength(350);
            builder.Property(x => x.HaberFoto).HasMaxLength(70);
            builder.Property(x => x.HaberOnizlemeFoto).HasMaxLength(70);
            builder.Property(x => x.Icerik).HasMaxLength(1000).IsRequired();
            builder.HasOne(x => x.kullanici).WithMany(x => x.haberler).HasForeignKey(x => x.KullaniciID);
            builder.HasOne(x => x.kategori).WithMany(x => x.haberList).HasForeignKey(x => x.KategoriID);
        }
    }
}
