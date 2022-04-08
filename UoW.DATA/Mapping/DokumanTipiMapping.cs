using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class DokumanTipiMapping : IEntityTypeConfiguration<DokumanTipi>
    {
        public void Configure(EntityTypeBuilder<DokumanTipi> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.TipAdi).HasMaxLength(50);
            builder.Property(x => x.Pozisyon).HasMaxLength(50);
        }
    }
}
