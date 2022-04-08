using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class SepetMapping : IEntityTypeConfiguration<Sepet>
    {
        public void Configure(EntityTypeBuilder<Sepet> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.UrunSepet).WithMany(x => x.sepetUrun).HasForeignKey(x => x.UrunID);
            builder.HasOne(x => x.siparisSepet).WithMany(x => x.sepetSiparis).HasForeignKey(x => x.SiparisID);
            builder.HasOne(x => x.BayiSepet).WithMany(x => x.sepetListBayi).HasForeignKey(x=> x.BayiID);
        }
    }
}
