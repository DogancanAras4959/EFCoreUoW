using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.LOG.Models;

namespace UoW.LOG.MappingLog
{
    public class YoneticiMapping : IEntityTypeConfiguration<Yonetici>
    {
        public void Configure(EntityTypeBuilder<Yonetici> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.KullaniciAdi).HasMaxLength(50);
        }
    }
}
