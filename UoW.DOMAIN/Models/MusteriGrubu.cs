using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("MUSTERI_GRUBU")]
    public class MusteriGrubu : BaseEntity
    {

        [Required(ErrorMessage = "Grup Adı zorunludur")]
        public string GrubAdi { get; set; }
        public bool Durum { get; set; }

        [Required(ErrorMessage = "İskonto Oranı zorunludur")]
        public decimal IskontoOrani { get; set; }
        public string  Aciklama { get; set; }

        public string GrupIcon { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public int YoneticiID { get; set; }
        public virtual Kullanici kullaniciGrup { get; set; }
        public virtual ICollection<Bayi> bayiMusteriGrublari { get; set; }
        public virtual ICollection<MusteriGrupOzelUrun> urunOzelUrun { get; set; }
        public virtual ICollection<MusteriGrupKargo> kargoOzelKargo { get; set; }

    }
}
