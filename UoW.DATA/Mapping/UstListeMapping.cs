using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class UstListeMapping : IEntityTypeConfiguration<UstListe>
    {
        public void Configure(EntityTypeBuilder<UstListe> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ListeAdi);
            builder.HasOne(x => x.bayiListUst).WithMany(x => x.ustLists).HasForeignKey(x => x.BayiID);
        }
    }
}
