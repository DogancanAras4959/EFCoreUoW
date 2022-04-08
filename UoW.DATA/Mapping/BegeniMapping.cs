using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class BegeniMapping : IEntityTypeConfiguration<Begeni>
    {
        public void Configure(EntityTypeBuilder<Begeni> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.bayiBegeni).WithMany(x => x.begeniler).HasForeignKey(x => x.BayiID);
            builder.HasOne(x => x.urunBegeni).WithMany(x => x.begeniler).HasForeignKey(x => x.UrunID);
        }
    }
}
