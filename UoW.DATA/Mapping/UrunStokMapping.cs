using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class UrunStokMapping : IEntityTypeConfiguration<UrunStok>
    {
        public void Configure(EntityTypeBuilder<UrunStok> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.bayiStok).WithMany(x => x.listBayiStok).HasForeignKey(x => x.BayiID);
            builder.HasOne(x => x.bayiUrun).WithMany(x => x.listUrunStok).HasForeignKey(x => x.UrunID);
        }
    }
}
