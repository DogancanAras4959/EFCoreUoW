using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class OdemeDurumMapping : IEntityTypeConfiguration<OdemeDurum>
    {
        public void Configure(EntityTypeBuilder<OdemeDurum> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.OnayKodu).HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.odemeSekliDurum).WithMany(x => x.listOdemeDurum).HasForeignKey(x => x.OdemeTipiID);
        }
    }
}
