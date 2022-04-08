using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class KargoDetayMapping : IEntityTypeConfiguration<KargoDetay>
    {
        public void Configure(EntityTypeBuilder<KargoDetay> builder)
        {
            builder.Property(x => x.ID).UseIdentityColumn();
        }
    }
}
