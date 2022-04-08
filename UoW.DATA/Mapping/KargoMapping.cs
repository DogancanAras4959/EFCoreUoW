using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class KargoMapping : IEntityTypeConfiguration<Kargo>
    {
        public void Configure(EntityTypeBuilder<Kargo> builder)
        {
            builder.Property(x => x.ID).UseIdentityColumn();
            builder.Property(x => x.SatinalmaNotu).HasMaxLength(250);
            builder.HasOne(x => x.bayiAdresler).WithMany(x => x.kargoAdres).HasForeignKey(x => x.BayiAdresID);
            builder.HasOne(x => x.kargoDetayi).WithMany(x => x.kargoList).HasForeignKey(x => x.KargoDetayID);
            builder.HasOne(x => x.siparis).WithMany(x => x.kargoSiparis).HasForeignKey(x => x.SiparisID);
            builder.HasOne(x => x.bayiAdresler).WithMany(x => x.kargoAdres).HasForeignKey(x => x.BayiAdresID);
            builder.HasOne(x => x.kargoFirmaDetay).WithMany(x => x.kargoDetaylari).HasForeignKey(x => x.KargoFirmaID);

        }
    }
}
