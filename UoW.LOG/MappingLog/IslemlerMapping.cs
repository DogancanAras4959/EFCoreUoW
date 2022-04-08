using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.LOG.Models;

namespace UoW.LOG.MappingLog
{
    public class IslemlerMapping : IEntityTypeConfiguration<Islemler>
    {
        public void Configure(EntityTypeBuilder<Islemler> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.IslemAdi).HasMaxLength(50);
        }
    }
}
