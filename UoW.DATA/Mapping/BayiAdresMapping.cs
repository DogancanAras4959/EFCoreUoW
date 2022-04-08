using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class BayiAdresMapping : IEntityTypeConfiguration<BayiAdresler>
    {
        public void Configure(EntityTypeBuilder<BayiAdresler> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Adres).HasMaxLength(500).IsRequired();
            builder.Property(x => x.AdresAdi).HasMaxLength(50).IsRequired();
            builder.Property(x => x.SiparisNotu).HasMaxLength(50).IsRequired();
          
            builder.HasOne(x => x.sehirAdres).WithMany(x => x.sehirAdresler).HasForeignKey(x => x.SehirID);
            builder.HasOne(x => x.ilceAdres).WithMany(x => x.ilceAdresler).HasForeignKey(x => x.IlceID);
        }
    }
}
