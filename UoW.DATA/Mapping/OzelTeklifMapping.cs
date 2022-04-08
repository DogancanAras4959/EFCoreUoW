using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class OzelTeklifMapping : IEntityTypeConfiguration<OzelTeklif>
    {
        public void Configure(EntityTypeBuilder<OzelTeklif> builder)
        {
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.siparisOzelTeklif).WithMany(x => x.ozelTekliflerSepetList).HasForeignKey(x => x.SiparisID);
            builder.HasOne(x => x.bayiOzelTeklif).WithMany(x => x.ozelTeklifListBayi).HasForeignKey(x => x.BayiID);
        }
    }
}
