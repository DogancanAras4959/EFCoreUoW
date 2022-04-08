using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class SiparisMapping : IEntityTypeConfiguration<Siparis>
    {
        public void Configure(EntityTypeBuilder<Siparis> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.bayiSiparis).WithMany(x => x.siparisBayi).HasForeignKey(x => x.BayiID);
            builder.HasOne(x => x.odemeSekliSiparis).WithMany(x => x.listSiparis).HasForeignKey(x => x.OdemeSekliID);
            builder.HasOne(x => x.bayiAdresleri).WithMany(x => x.siparisBayiAdres).HasForeignKey(x => x.BayiAdresID);
            builder.HasOne(x => x.siparisDurumSiparis).WithMany(x => x.siparisList).HasForeignKey(x => x.SiparisDurumID);
        }
    }
}
