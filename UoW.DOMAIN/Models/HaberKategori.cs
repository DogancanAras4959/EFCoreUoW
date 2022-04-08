using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("HABER_KATEGORI")]
    public class HaberKategori: BaseEntity
    {
        [Required(ErrorMessage = "Haber Kategorisi'nin Adı zorunludur")]
        public string HaberKategoriAdi { get; set; }
        
        [ForeignKey("kullanici")]
        public int KullaniciID { get; set; }
        public bool AtkifMi { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        
        public ICollection<Haber> haberList { get; set; }
        public Kullanici kullanici { get; set; }
    }
}
