using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UoW.DOMAIN.Models
{
    [Table("KULLANICI")]
    public class Kullanici : BaseEntity
    {
        [Required(ErrorMessage = "Kullanıcı Adı zorunludur")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string Sifre { get; set; }
        public string ProfilResim { get; set; }
        public int RolID { get; set; }
        public bool Durum { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<BasvuruAyar> basvuruAyar { get; set; }
        public virtual ICollection<Bayi> kullaniciBayiler { get; set; }
        public virtual ICollection<MusteriGrubu> musteriGrublar { get; set; }
        public virtual ICollection<Urun> urunler { get; set; }
        public virtual ICollection<StokBirimi> stokBirimi { get; set; }
        public virtual ICollection<Kategori> kategoriler { get; set; }
        public virtual ICollection<FaturaTip> faturaTipKullanici { get; set; }
        public virtual ICollection<Slider> sliderKullanici { get; set; }
        public virtual ICollection<SiteInfo> siteInfo { get; set; }
        public virtual ICollection<HaberKategori> haberKategoriler { get; set; }
        public virtual ICollection<Haber> haberler { get; set; }
        public virtual ICollection<Dokuman> dokumanlar { get; set; }
    }
}
