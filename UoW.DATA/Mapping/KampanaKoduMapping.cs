using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class KampanaKoduMapping : IEntityTypeConfiguration<KampanyaKoduSliderBanner>
    {
        public void Configure(EntityTypeBuilder<KampanyaKoduSliderBanner> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Kod).HasMaxLength(20);
        }
    }
}
