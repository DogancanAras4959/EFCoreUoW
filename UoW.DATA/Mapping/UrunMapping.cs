using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class UrunMapping : IEntityTypeConfiguration<Urun>
    {
        public void Configure(EntityTypeBuilder<Urun> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.UrunAdi).HasMaxLength(75);
            builder.Property(x => x.UrunSpot).HasMaxLength(175);
            builder.Property(x => x.Aciklama).HasMaxLength(375);
            builder.Property(x => x.UrunBarkodNo).HasMaxLength(20);
            builder.Property(x => x.UrunNo).HasMaxLength(20);
            builder.Property(x => x.Resim).HasMaxLength(35);
            builder.HasOne(x => x.kullanici).WithMany(x => x.urunler).HasForeignKey(x => x.YoneticiID);
            builder.HasOne(x => x.kategori).WithMany(x => x.urunler).HasForeignKey(x => x.KategoriID);
            builder.HasOne(x => x.marka).WithMany(x => x.urunler).HasForeignKey(x => x.MarkaID);
        }
    }
}
