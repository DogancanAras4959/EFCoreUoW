using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class SliderBannerItemMapping : IEntityTypeConfiguration<SliderBannerItem>
    {
     
        public void Configure(EntityTypeBuilder<SliderBannerItem> builder)
        {
            builder.Property(x => x.ItemName).HasMaxLength(150);
            builder.Property(x => x.ItemDesc).HasMaxLength(350);
            builder.Property(x => x.ItemImage).HasMaxLength(50);
            builder.HasOne(x => x.sliderBannerItemSlider).WithMany(x => x.sliderBannerItems).HasForeignKey(x => x.SliderID);
        }
    }
}
