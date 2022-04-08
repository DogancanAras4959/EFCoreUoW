using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class EtiketMapping : IEntityTypeConfiguration<Etiketler>
    {
        public void Configure(EntityTypeBuilder<Etiketler> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.EtiketAdi).HasMaxLength(30);
        }
    }
}
