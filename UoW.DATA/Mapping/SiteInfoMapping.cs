using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class SiteInfoMapping : IEntityTypeConfiguration<SiteInfo>
    {
        public void Configure(EntityTypeBuilder<SiteInfo> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Baslik).HasMaxLength(70).IsRequired();
            builder.Property(x => x.LinkAdi).HasMaxLength(70).IsRequired();
            builder.Property(x => x.Icerik).HasMaxLength(100).IsRequired();
            builder.HasOne(x => x.kullanici).WithMany(x => x.siteInfo).HasForeignKey(x => x.YoneticiID);
        }
    }
}
