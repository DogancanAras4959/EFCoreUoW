using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class ListMapping : IEntityTypeConfiguration<Liste>
    {
        public void Configure(EntityTypeBuilder<Liste> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.urunListe).WithMany(x => x.lists).HasForeignKey(x => x.UrunID);
            builder.HasOne(x => x.bayiList).WithMany(x => x.lists).HasForeignKey(x => x.BayiID);
        }
    }
}
