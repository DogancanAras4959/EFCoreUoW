using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class KargoFirmalarMapping : IEntityTypeConfiguration<KargoFirmalar>
    {
        public void Configure(EntityTypeBuilder<KargoFirmalar> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.KargoAdi).HasMaxLength(50);
            builder.Property(x => x.KargoAciklama).HasMaxLength(500);
        }
    }
}
