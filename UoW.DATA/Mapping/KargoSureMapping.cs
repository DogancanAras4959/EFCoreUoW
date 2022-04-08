using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class KargoSureMapping : IEntityTypeConfiguration<KargoSure>
    {
        public void Configure(EntityTypeBuilder<KargoSure> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.kargoSüreBayi).WithMany(x => x.kargoSureBayi).HasForeignKey(x => x.BayiID);
            builder.HasOne(x => x.kargoSureKargo).WithMany(x => x.kargoSureKargo).HasForeignKey(x => x.KargoID);
        }
    }
}
