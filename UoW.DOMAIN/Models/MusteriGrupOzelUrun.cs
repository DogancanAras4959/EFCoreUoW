using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("MUSTERI_GRUP_OZEL_URUN")]
    public class MusteriGrupOzelUrun : BaseEntity
    {
        public int MusteriGrupID { get; set; }
        public int UrunID { get; set; }
        public bool Durum { get; set; }
        public DateTime Eklenme { get; set; }

        [ForeignKey("MusteriGrupID")]
        public virtual MusteriGrubu musteriGrubuOzelUrun { get; set; }
        
        [ForeignKey("UrunID")]
        public virtual Urun urunOzelUrun { get; set; }
    }
}
