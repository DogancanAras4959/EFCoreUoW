using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class MusteriGrupUrunMapping : IEntityTypeConfiguration<MusteriGrupOzelUrun>
    {
        public void Configure(EntityTypeBuilder<MusteriGrupOzelUrun> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.musteriGrubuOzelUrun).WithMany(x => x.urunOzelUrun).HasForeignKey(x => x.MusteriGrupID);
            builder.HasOne(x => x.urunOzelUrun).WithMany(x => x.urunlerMusteriGrupOzel).HasForeignKey(x => x.UrunID);
        }
    }
}
