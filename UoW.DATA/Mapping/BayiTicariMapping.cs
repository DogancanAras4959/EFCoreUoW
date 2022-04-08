using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class BayiTicariMapping : IEntityTypeConfiguration<BayiTicari>
    {
        public void Configure(EntityTypeBuilder<BayiTicari> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Adres).HasMaxLength(500).IsRequired();
            builder.Property(x => x.FirmaUnvani).HasMaxLength(50).IsRequired();
            builder.Property(x => x.FirmaTipi).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PostaKodu).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Telefon).HasMaxLength(11).IsRequired();
            builder.Property(x => x.VergiNo).HasMaxLength(50).IsRequired();
            builder.Property(x => x.VergiDairesi).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Faks).HasMaxLength(30).IsRequired();
            builder.Property(x => x.IBanNoEUR).HasMaxLength(30).IsRequired();
            builder.Property(x => x.IBanNoTL).HasMaxLength(30).IsRequired();
            builder.Property(x => x.IBanNoUSD).HasMaxLength(30).IsRequired();
            builder.Property(x => x.MuhasebeMutabakatEmail).HasMaxLength(30).IsRequired();
            builder.Property(x => x.MuhasebeMutabakatTel).HasMaxLength(30).IsRequired();
            builder.HasOne(x => x.bayiBayiTicari).WithMany(x => x.bayiTicari).HasForeignKey(x => x.BayiID);
            builder.HasOne(x => x.firmaYetkili).WithMany(x => x.ticariList).HasForeignKey(x => x.firmaYetkili);
        }
    }
}
