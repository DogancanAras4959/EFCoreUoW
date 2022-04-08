using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class StokBirimiMapping : IEntityTypeConfiguration<StokBirimi>
    {
        public void Configure(EntityTypeBuilder<StokBirimi> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.kullaniciBirim).WithMany(x => x.stokBirimi).HasForeignKey(x => x.YoneticiID);
        }
    }
}
