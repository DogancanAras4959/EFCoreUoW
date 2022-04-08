using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class MusteriGrupMapping : IEntityTypeConfiguration<MusteriGrubu>
    {
        public void Configure(EntityTypeBuilder<MusteriGrubu> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.GrubAdi).HasMaxLength(20).IsRequired();
            builder.Property(x => x.IskontoOrani).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.Aciklama).HasMaxLength(500).IsRequired();
            builder.Property(x => x.GrupIcon).HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.kullaniciGrup).WithMany(x => x.musteriGrublar).HasForeignKey(x => x.YoneticiID);
        }
    }
}
