using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("STOK_BIRIMI")]
    public class StokBirimi : BaseEntity
    {
        [Required(ErrorMessage = "Birim Adı zorunludur")]
        public string BirimAdi { get; set; }
        public bool Durum { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public int YoneticiID { get; set; }
        public Kullanici kullaniciBirim { get; set; }
        public virtual ICollection<Urun> urunlerBirim { get; set; }
    }
}
