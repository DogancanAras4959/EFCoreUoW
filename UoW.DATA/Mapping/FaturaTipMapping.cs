using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class FaturaTipMapping : IEntityTypeConfiguration<FaturaTip>
    {
        public void Configure(EntityTypeBuilder<FaturaTip> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.TipAdi).HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.KullaniciFatura).WithMany(x => x.faturaTipKullanici).HasForeignKey(x=> x.KullaniciID);
        }
    }
}
