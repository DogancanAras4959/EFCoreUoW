using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class YetkiliMapping : IEntityTypeConfiguration<Yetkililer>
    {
        public void Configure(EntityTypeBuilder<Yetkililer> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Email).HasMaxLength(20).IsRequired();
            builder.Property(x => x.TelNo).HasMaxLength(20).IsRequired();
            builder.Property(x => x.YetkiliAdi).HasMaxLength(20).IsRequired();
            builder.Property(x => x.YetkiliFoto).HasMaxLength(75);
            builder.HasOne(x => x.bayiYetkilisi).WithMany(x => x.yetkililer).HasForeignKey(x => x.BayiID);
        }
    }
}
