using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("KATEGORİLER")]
    public class Kategori : BaseEntity
    {
        [Required(ErrorMessage = "Kategori Kodu zorulunudur")]
        public string KategoriKodu { get; set; }
        [Required(ErrorMessage = "Kategori Adı zorunludur")]
        public string KategoriAdi { get; set; }
        public bool Durum { get; set; }
        public string KategoriFoto { get; set; }
        public string KategoriAciklama { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public int? UstKategori { get; set; }
        public int YoneticiID { get; set; }     
        public virtual Kullanici kullanici { get; set; }
        public virtual ICollection<Urun> urunler { get; set; }
    }
}
