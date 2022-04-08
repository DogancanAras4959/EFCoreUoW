using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class CuzdanIslemlerMapping : IEntityTypeConfiguration<CuzdanIslemler>
    {
        public void Configure(EntityTypeBuilder<CuzdanIslemler> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.IslemNo).HasMaxLength(50);
            builder.HasOne(x => x.cuzdanHesabi).WithMany(x => x.cuzdanIslemlers).HasForeignKey(x => x.CuzdanID);
            builder.HasOne(x => x.odemeSekliCuzdan).WithMany(x => x.listCuzdenIslemler).HasForeignKey(x => x.OdemeSekliID);
        }
    }
}
