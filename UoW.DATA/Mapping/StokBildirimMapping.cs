using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class StokBildirimMapping : IEntityTypeConfiguration<StokBildirim>
    {
        public void Configure(EntityTypeBuilder<StokBildirim> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.urun).WithMany(x => x.bildirimUrun).HasForeignKey(x => x.UrunId);
            builder.HasOne(x => x.Bayi).WithMany(x => x.sotkBildirimBayi).HasForeignKey(x => x.BayiId);
        }
    }
}
