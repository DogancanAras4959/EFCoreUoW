using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("URUNLER")]
    public class Urun : BaseEntity
    {
        [Required(ErrorMessage = "Ürün Adı zorunludur")]
        public string UrunAdi { get; set; }
        public string UrunSpot { get; set; }

        [Required(ErrorMessage = "Ürün Numarası zorunludur")]
        public string UrunNo { get; set; }

        [Required(ErrorMessage = "Ürün Barkod Numarası zorunludur")]
        public string UrunBarkodNo { get; set; }
        public int Adet { get; set; }
        public int MaxAdet { get; set; }
        public int MinAdet { get; set; }

        [DataType("decimal(18,2)")]
        [Required(ErrorMessage = "Ürün Fiyatı zorunludur")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Fiyat { get; set; }
        public string FiyatBirimi { get; set; }
        public decimal KDV { get; set; }

        [DataType("decimal(18,2)")]
        public decimal KDVFiyat { get; set; }

        [DataType("decimal(18,2)")]
        public decimal? IndirimOrani { get; set; }
        public bool OzelMi { get; set; }

        [DataType("decimal(18,2)")]
        public decimal IndirimliFiyat { get; set; }
        public string Aciklama { get; set; }

        [Required(ErrorMessage ="Kategori verisi zorunludur, kategori seçiniz")]
        [ForeignKey("kategori")]
        public int KategoriID { get; set; }
        
        [ForeignKey("kullanici")]
        public int YoneticiID { get; set; }

        public int? UstUrunID { get; set; }

        [Required(ErrorMessage = "Web Servis Kodu zorunludur")]
        public string WebServisKodu { get; set; }

        [Required(ErrorMessage ="Marka seçiniz, marka zorunludur")]
        [ForeignKey("marka")]
        public int MarkaID { get; set; }

        [Required(ErrorMessage = "Stok birimi seçiniz, stok birimi zorunludur")]
        [ForeignKey("stokbirimi")]
        public int StokBirimID { get; set; }
        public string Etiketler { get; set; }
        public bool Durum { get; set; }
        public bool BildirimVarmi { get; set; }
        public string Resim { get; set; }
        public string DetayResim { get; set; }
        public double En { get; set; }
        public double Boy { get; set; }
        public double Derinlik { get; set; }
        public double Agirlik { get; set; }
        public double Desi { get; set; }
        public virtual Kategori kategori { get; set; }
        public virtual Kullanici kullanici { get; set; }
        public virtual Marka marka { get; set; }
        public virtual StokBirimi stokbirimi { get; set; }
        public virtual ICollection<UrunResimler> urunResimler { get; set; }
        public virtual ICollection<MusteriGrupOzelUrun> urunlerMusteriGrupOzel { get; set; }
        public virtual ICollection<EtiketGonderi> etiketlerGonderi { get; set; }
        public virtual ICollection<Sepet> sepetUrun { get; set; }
        public virtual ICollection<UrunStok> listUrunStok { get; set; }
        public virtual ICollection<Begeni> begeniler { get; set; }
        public virtual ICollection<Liste> lists { get; set; }
        public virtual ICollection<StokBildirim> bildirimUrun { get; set; }
        public virtual ICollection<SliderUrun> urunSliderUrun { get; set; }
        public virtual ICollection<UrunRenk> urunRenkUrun { get; set; }

    }
}
