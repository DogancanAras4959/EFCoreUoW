using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class SliderMapping : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(x => x.SliderName).HasMaxLength(50);
            builder.Property(x => x.SliderDesc).HasMaxLength(250);
            builder.HasOne(x => x.sliderKullanici).WithMany(x => x.sliderKullanici).HasForeignKey(x => x.YoneticiID);
        }
    }
}
