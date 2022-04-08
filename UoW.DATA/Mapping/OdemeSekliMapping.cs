using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class OdemeSekliMapping : IEntityTypeConfiguration<OdemeSekli>
    {
        public void Configure(EntityTypeBuilder<OdemeSekli> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Odeme).HasMaxLength(50).IsRequired();
        }
    }
}
