using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("MUSTERI_GRUP_KARGO")]
    public class MusteriGrupKargo : BaseEntity
    {
        public int KargoID { get; set; }
        public int MusteriGrupID { get; set; }
        public bool Durum { get; set; }
        public DateTime Eklenme { get; set; }

        [ForeignKey("KargoID")]
        public KargoFirmalar Kargo { get; set; }

        [ForeignKey("MusteriGrupID")]
        public MusteriGrubu musteriGrubu { get; set; }
    }
}
