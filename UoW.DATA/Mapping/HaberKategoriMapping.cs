using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class HaberKategoriMapping : IEntityTypeConfiguration<HaberKategori>
    {
        public void Configure(EntityTypeBuilder<HaberKategori> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.HaberKategoriAdi).HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.kullanici).WithMany(x => x.haberKategoriler).HasForeignKey(x => x.KullaniciID);
        }
    }
}
