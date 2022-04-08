using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class BultenMapping : IEntityTypeConfiguration<Bulten>
    {
        public void Configure(EntityTypeBuilder<Bulten> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.EmailAdresi).HasMaxLength(50);
            builder.Property(x => x.TelefonNo).HasMaxLength(50);
            builder.Property(x => x.AdinizSoyadiniz).HasMaxLength(50);
            builder.HasOne(x => x.yetkili).WithMany(x => x.bultenler).HasForeignKey(x => x.YetkiliID);
        }
    }
}

