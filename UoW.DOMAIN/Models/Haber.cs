using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("HABER")]
    public class Haber : BaseEntity
    {
        [Required(ErrorMessage = "Haber Adı zorunludur")]
        public string HaberAdi { get; set; }
        public string HaberSpot { get; set; }
        public string HaberFoto { get; set; }
        public string HaberOnizlemeFoto { get; set; }
        public bool AktifMi { get; set; }

        [Required(ErrorMessage = "Haber İçeriği zorunludur")]
        public string Icerik { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public DateTime EklenmeTarih { get; set; }     
        [ForeignKey("kategori")]

        [Required(ErrorMessage = "Haber Kategorisi zorunludur")]
        public int KategoriID { get; set; }
        [ForeignKey("Kullanici")]
        public int KullaniciID { get; set; }
        public HaberKategori kategori { get; set; }
        public Kullanici kullanici { get; set; }
        //public ICollection<HaberEtiketHaber> haberEtiketiHaber {get;set;}
    }
}
