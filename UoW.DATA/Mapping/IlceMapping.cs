using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class IlceMapping : IEntityTypeConfiguration<Ilce>
    {
        public void Configure(EntityTypeBuilder<Ilce> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.IlceAdi).HasMaxLength(50);
            builder.HasOne(x => x.sehirIlce).WithMany(x => x.ilceler).HasForeignKey(x => x.SehirID);
        }
    }
}
