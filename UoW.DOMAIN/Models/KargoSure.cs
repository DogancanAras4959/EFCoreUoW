using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("KARGO_SURE")]
    public class KargoSure : BaseEntity
    {
        public int BayiID { get; set; }
        public int KargoID { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public bool AktifMi { get; set; }

        [ForeignKey("BayiID")]
        public Bayi kargoSüreBayi { get; set; }

        [ForeignKey("KargoID")]
        public KargoFirmalar kargoSureKargo { get; set; }
    }
}
