using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UoW.DOMAIN.Models;

namespace UoW.DATA.Mapping
{
    public class KullaniciMapping : IEntityTypeConfiguration<Kullanici>
    {
        public void Configure(EntityTypeBuilder<Kullanici> builder)
        {
            builder.HasKey(x => x.ID);
            //builder.Property(X => X.ID).UseIdentityColumn();
            builder.Property(x => x.KullaniciAdi).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ProfilResim).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Sifre).HasMaxLength(10).IsRequired();
            builder.HasOne(x => x.Rol).WithMany(x => x.kullanicis).HasForeignKey(x => x.RolID);
        }
    }
}
