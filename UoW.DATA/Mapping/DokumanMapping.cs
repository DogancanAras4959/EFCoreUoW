using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class DokumanMapping : IEntityTypeConfiguration<Dokuman>
    {
        public void Configure(EntityTypeBuilder<Dokuman> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.DosyaYolu).HasMaxLength(50);
            builder.Property(x => x.Onizleme).HasMaxLength(50);
            builder.HasOne(x => x.dokuman).WithMany(x => x.dokumans).HasForeignKey(x => x.DokumanTipi);
            builder.HasOne(x => x.kullanici).WithMany(x => x.dokumanlar).HasForeignKey(x => x.YoneticiID);
        }
    }
}
