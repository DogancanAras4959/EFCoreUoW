using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.LOG.Models;

namespace UoW.LOG.MappingLog
{
    public class DurumlarMapping : IEntityTypeConfiguration<Durumlar>
    {
        public void Configure(EntityTypeBuilder<Durumlar> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.DurumAdi).HasMaxLength(50);
        }
    }
}
