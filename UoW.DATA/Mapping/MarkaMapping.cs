using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class MarkaMapping : IEntityTypeConfiguration<Marka>
    {
        public void Configure(EntityTypeBuilder<Marka> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.MarkaAciklama).HasMaxLength(75);
            builder.Property(x => x.MarkaAdi).HasMaxLength(75);
        }
    }
}
