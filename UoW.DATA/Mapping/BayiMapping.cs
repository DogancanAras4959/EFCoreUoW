using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class BayiMapping : IEntityTypeConfiguration<Bayi>
    {
        public void Configure(EntityTypeBuilder<Bayi> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.KullaniciAdi).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Logo).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Sifre).HasMaxLength(20).IsRequired();
            builder.Property(x => x.UyeNo).HasMaxLength(20).IsRequired();
            builder.Property(x => x.BayiAdi).HasMaxLength(40).IsRequired();
            builder.Property(x => x.UyeNo).HasMaxLength(20).IsRequired();
            builder.HasOne(x => x.musteriGrubu).WithMany(x => x.bayiMusteriGrublari).HasForeignKey(x => x.BayiGrubuID);
            builder.HasOne(x => x.kullanici).WithMany(x => x.kullaniciBayiler).HasForeignKey(x => x.YoneticiID);
            builder.HasOne(x => x.odeme).WithMany(x => x.listBayi).HasForeignKey(x => x.OdemeID);
            builder.HasOne(x => x.kargoBayiFirma).WithMany(x => x.bayiKargolarList).HasForeignKey(x => x.KargoFirmasiID);
        }
    }
}
