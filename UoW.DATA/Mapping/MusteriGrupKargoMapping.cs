using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class MusteriGrupKargoMapping : IEntityTypeConfiguration<MusteriGrupKargo>
    {
        public void Configure(EntityTypeBuilder<MusteriGrupKargo> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.musteriGrubu).WithMany(x => x.kargoOzelKargo).HasForeignKey(x => x.MusteriGrupID);
            builder.HasOne(x => x.Kargo).WithMany(x => x.musteriGrupKargoMusteri).HasForeignKey(x => x.KargoID);
        }
    }
}
