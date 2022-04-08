using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class FaturaMapping : IEntityTypeConfiguration<Fatura>
    {
        public void Configure(EntityTypeBuilder<Fatura> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.FaturaNo).HasMaxLength(50);
            builder.HasOne(x => x.faturaTipi).WithMany(x => x.faturalar).HasForeignKey(x=> x.FaturaTipiID);
            builder.HasOne(x => x.bayiFatura).WithMany(x => x.faturaBayi).HasForeignKey(x => x.BayiID);
            builder.HasOne(x => x.bayiAdresler).WithMany(x => x.faturaBayiAdres).HasForeignKey(x => x.BayiAdresID);
        }
    }
}
