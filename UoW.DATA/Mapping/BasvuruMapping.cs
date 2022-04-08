using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class BasvuruMapping : IEntityTypeConfiguration<Basvuru>
    {
        public void Configure(EntityTypeBuilder<Basvuru> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.BasvuruNo).HasMaxLength(500).IsRequired();
            builder.Property(x => x.BayiAdi).HasMaxLength(50).IsRequired();
            builder.Property(x => x.BayiUnvani).HasMaxLength(50).IsRequired();
            builder.Property(x => x.basvuruFoto).HasMaxLength(50).IsRequired();
            builder.Property(x => x.EmailAdresi).HasMaxLength(11).IsRequired();
            builder.Property(x => x.TelNo).HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.basvuruDurum).WithMany(x => x.basvurular).HasForeignKey(x => x.BasvuruDurumID);
        }
    }
}
