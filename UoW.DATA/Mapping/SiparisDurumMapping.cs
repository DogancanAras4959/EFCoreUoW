using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class SiparisDurumMapping : IEntityTypeConfiguration<SiparisDurum>
    {
        public void Configure(EntityTypeBuilder<SiparisDurum> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Durum).HasMaxLength(70);
        
        }
    }
}
