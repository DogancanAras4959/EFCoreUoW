using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("MARKA")]
    public class Marka : BaseEntity
    {
        [Required(ErrorMessage = "Marka Adı zorunludur")]
        public string MarkaAdi { get; set; }
        public string MarkaAciklama { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public string MarkaResim { get; set; }
        public bool Durum { get; set; }

        public int? UstMarkaID { get; set; }
        public int YoneticiID { get; set; }
        public virtual Kullanici kullanici { get; set; }
        public virtual ICollection<Urun> urunler { get; set; }
    }
}
