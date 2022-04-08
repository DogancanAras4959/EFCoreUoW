using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class SliderUrunMapping : IEntityTypeConfiguration<SliderUrun>
    {
        public void Configure(EntityTypeBuilder<SliderUrun> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.sliderBannerItemSliderUrun).WithMany(x => x.sliderBannerItemSliderUrun).HasForeignKey(x => x.SliderBannerID);
            builder.HasOne(x => x.urunSliderUrun).WithMany(x => x.urunSliderUrun).HasForeignKey(x => x.UrunID);
            builder.HasOne(x => x.sliderItemSliderUrun).WithMany(x => x.sliderItemSliderUrunList).HasForeignKey(x => x.SliderItemID);
        }
    }
}
