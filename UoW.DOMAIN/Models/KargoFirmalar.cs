using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("KARGO_FIRMALARI")]
    public class KargoFirmalar : BaseEntity
    {
        public string KargoFoto { get; set; }

        [Required(ErrorMessage = "Kargo Adı zorunludur")]
        public string KargoAdi { get; set; }
        public bool Durum { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }

        [DataType("decimal(18,2)")]
        [Required(ErrorMessage = "Kargo Ücreti zorunludur")]
        public decimal KargoUcret { get; set; }

        [Required(ErrorMessage = "Kargo Açıklaması zorunludur")]
        public string KargoAciklama { get; set; }
        public ICollection<Kargo> kargoDetaylari { get; set; }
        public virtual List<Bayi> bayiKargolarList { get; set; }
        public ICollection<KargoSure> kargoSureKargo { get; set; }
        public ICollection<MusteriGrupKargo> musteriGrupKargoMusteri { get; set; }

    }
}
