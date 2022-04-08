using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class SliderItemMapping : IEntityTypeConfiguration<SliderItem>
    {
        public void Configure(EntityTypeBuilder<SliderItem> builder)
        {
            builder.Property(x => x.ItemName).HasMaxLength(150);
            builder.Property(x => x.ItemDesc).HasMaxLength(350);
            builder.Property(x => x.ItemImage).HasMaxLength(50);
            builder.HasOne(x => x.sliderItemSlider).WithMany(x => x.sliderItems).HasForeignKey(x => x.SliderID);
        }
    }
}
