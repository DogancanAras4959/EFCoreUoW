using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.LOG.Models;

namespace UoW.LOG.MappingLog
{
    public class LoglarMapping : IEntityTypeConfiguration<Loglar>
    {
        public void Configure(EntityTypeBuilder<Loglar> builder)
        {

            builder.HasKey(x => x.ID);
            builder.Property(x => x.Action).HasMaxLength(50);
            builder.Property(x => x.Controller).HasMaxLength(50);
            builder.HasOne(x => x.yonetici).WithMany(x => x.loglars).HasForeignKey(x => x.YoneticiID);
            builder.HasOne(x => x.durumlar).WithMany(x => x.loglars).HasForeignKey(x => x.DurumID);
            builder.HasOne(x => x.islemler).WithMany(x => x.loglars).HasForeignKey(x => x.IslemID);
        }
    }
}
