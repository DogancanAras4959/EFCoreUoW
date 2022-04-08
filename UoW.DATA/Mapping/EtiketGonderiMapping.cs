using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class EtiketGonderiMapping : IEntityTypeConfiguration<EtiketGonderi>
    {
        public void Configure(EntityTypeBuilder<EtiketGonderi> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.etiketler).WithMany(x => x.etiketlerGonderi).HasForeignKey(x => x.EtiketID);
            builder.HasOne(x => x.urunler).WithMany(x => x.etiketlerGonderi).HasForeignKey(x => x.GonderiID);
        }
    }

}
