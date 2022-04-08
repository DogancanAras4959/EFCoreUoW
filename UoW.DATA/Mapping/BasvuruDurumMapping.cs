using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class BasvuruDurumMapping : IEntityTypeConfiguration<BasvuruDurum>
    {
        public void Configure(EntityTypeBuilder<BasvuruDurum> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.DurumAdi).HasMaxLength(20);     
        }
    }
}
