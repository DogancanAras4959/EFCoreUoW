using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UoW.CORE.Enums;
using UoW.DOMAIN;
using UoW.DOMAIN.Models;

namespace UoW.DATA
{
    public class UoWDbContext : DbContext
    {
        public UoWDbContext(DbContextOptions<UoWDbContext> options) : base(options)
        {

        }
        public DbSet<Kullanici> kullanicis { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Sehir> sehirs { get; set; }
        public DbSet<Ilce> ilces { get; set; }
        public DbSet<Bayi> bayis { get; set; }
        public DbSet<BayiTicari> bayiTicaris { get; set; }
        public DbSet<Yetkililer> yetkilers { get; set; }
        public DbSet<MusteriGrubu> musteriGrubs { get; set; }
        public DbSet<MusteriGrupOzelUrun> musteriGrubOzelUruns { get; set; }
        public DbSet<OdemeSekli> odemeSekli { get; set; }
        public DbSet<Bulten> bulten { get; set; }
        public DbSet<Urun> urunlers { get; set; }
        public DbSet<UrunResimler> urunResimlers { get; set; }
        public DbSet<Kategori> kategorilers { get; set; }
        public DbSet<Marka> markalars { get; set; }
        public DbSet<UrunFiyat> urunFiyats { get; set; }
        public DbSet<MusteriGrupOzelUrun> musteriGrupUrunOzel { get; set; }
        public DbSet<EtiketGonderi> etiketGonderiler { get; set; }
        public DbSet<Etiketler> etiketler { get; set; }
        public DbSet<StokBirimi> stokBirimlers { get; set; }
        public DbSet<Sepet> sepets { get; set; }
        public DbSet<Siparis> siparis { get; set; }
        public DbSet<Fatura> faturas { get; set; }
        public DbSet<FaturaTip> faturatips { get; set; }
        public DbSet<OdemeDurum> odemeDurum { get; set; }
        public DbSet<BayiAdresler> bayiAdresler { get; set; }
        public DbSet<Kargo> kargos { get; set; }
        public DbSet<KargoDetay> kargoDetays { get; set; }
        public DbSet<CuzdanIslemler> cuzdanIslemlers { get; set; }
        public DbSet<UrunStok> urunStoks { get; set; }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<SliderItem> sliderItems { get; set; }
        public DbSet<KargoFirmalar> kargoFirmalar { get; set; }
        public DbSet<Begeni> begenis { get; set; }
        public DbSet<Liste> lists { get; set; }
        public DbSet<SliderBannerItem> sliderItemsBanners { get; set; }
        public DbSet<KargoSure> kargoSures { get; set; }
        public DbSet<Basvuru> basvurus { get; set; }
        public DbSet<BasvuruDurum> basvuruDurums { get; set; }
        public DbSet<BasvuruAyar> basvuruAyars { get; set; }
        public DbSet<BasvuruDosyalar> basvuruDosyalars { get; set; }
        public DbSet<MusteriGrupKargo> musteriGrupKargos { get; set; }
        public DbSet<KampanyaKoduSliderBanner> kampanyaKods { get; set; }
        public DbSet<SliderUrun> sliderUruns { get; set; }      
        public DbSet<OzelTeklif> ozelteklifs { get; set; }      
        public DbSet<UstListe> ustListe { get; set; }
        public DbSet<Yetki> yetki { get; set; }
        public DbSet<RolYetki> rolYetki { get; set; }
        public DbSet<Haber> habers { get; set; }
        public DbSet<HaberKategori> haberKategoris { get; set; }
        public DbSet<SiteInfo> siteInfos { get; set; }
        public DbSet<SiparisDurum> siparisDurums { get; set; }
        public DbSet<Renk> renks { get; set; }
        public DbSet<UrunRenk> urunRenks { get; set; }
        public DbSet<Dokuman> dokumans { get; set; }
        public DbSet<DokumanTipi> dokumanTipi { get; set; }
        public DbSet<KrediKartBIN> krediKartBins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuzdanIslemler>().Property(x => x.IslemDurum).HasConversion(x => x.ToString(), y => (IslemDurum)Enum.Parse(typeof(IslemDurum), y));

            modelBuilder.Entity<StokBildirim>().Property(x => x.Durum).HasConversion(x => x.ToString(), y => (StokBildirimDurum)Enum.Parse(typeof(StokBildirimDurum), y));

            modelBuilder.Entity<OzelTeklif>().Property(x => x.TeklifDurum).HasConversion(x => x.ToString(), y => (OzelTeklifDurum)Enum.Parse(typeof(OzelTeklifDurum), y));

            base.OnModelCreating(modelBuilder);
        }
    }
}
