using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("BAYI")]
    public class Bayi : BaseEntity
    {
        [Required(ErrorMessage ="Bayi Adı zorunludur")]
        public string BayiAdi { get; set; }

        [Required(ErrorMessage = "Üye No zorunludur")]
        public string UyeNo { get; set; }
        public int BayiGrubuID { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public int OdemeID { get; set; }

        [DataType("decimal(18,2)")]
        [Required(ErrorMessage = "Risk Limiti zorunludur")]
        public decimal RiskLimiti { get; set; }
        public bool AltBayiHakki { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı zorunludur")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string Sifre { get; set; }
        public bool AktifMi { get; set; }
        public bool HesapKisitla { get; set; }
        public bool KargoYonet { get; set; } = true;
        public string Logo { get; set; }
        public int YoneticiID { get; set; }
        public int KargoFirmasiID { get; set; }
        public virtual MusteriGrubu musteriGrubu { get; set; }
        public virtual Kullanici kullanici { get; set; }

        [ForeignKey("KargoFirmasiID")]
        public virtual KargoFirmalar kargoBayiFirma { get; set; }

        [ForeignKey("OdemeID")]
        public OdemeSekli odeme { get; set; }
        public virtual ICollection<Yetkililer> yetkililer { get; set; }
        public virtual ICollection<BayiTicari> bayiTicari { get; set; }
        public virtual ICollection<Fatura> faturaBayi { get; set; }
        public virtual ICollection<Siparis> siparisBayi { get; set; }
        public virtual ICollection<Kargo> kargoBayi { get; set; }
        public virtual ICollection<Cuzdan> cuzdanlar { get; set; }
        public virtual ICollection<UrunStok> listBayiStok { get; set; }
        public virtual ICollection<Begeni> begeniler { get; set; }
        public virtual ICollection<Liste> lists { get; set; }
        public virtual ICollection<UstListe> ustLists { get; set; }
        public virtual ICollection<KargoSure> kargoSureBayi { get; set; }
        public virtual ICollection<OzelTeklif> ozelTeklifListBayi { get; set; }
        public virtual ICollection<StokBildirim> sotkBildirimBayi { get; set; }
        public virtual ICollection<Sepet> sepetListBayi { get; set; }
    }
}
