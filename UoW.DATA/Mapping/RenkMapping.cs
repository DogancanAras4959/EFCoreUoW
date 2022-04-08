using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class RenkMapping : IEntityTypeConfiguration<Renk>
    {
        public void Configure(EntityTypeBuilder<Renk> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.RenkAdi);
            builder.Property(x => x.RenkKodu);
        }
    }
}
