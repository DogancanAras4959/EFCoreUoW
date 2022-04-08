using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class UrunRenkMapping : IEntityTypeConfiguration<UrunRenk>
    {
        public void Configure(EntityTypeBuilder<UrunRenk> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.urun).WithMany(x => x.urunRenkUrun).HasForeignKey(x => x.UrunID);
            builder.HasOne(x => x.renk).WithMany(x => x.urunRenkRenk).HasForeignKey(x => x.RenkID);
        }
    }
}
