using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("KARGO")]
    public class Kargo : BaseEntity
    {
        public int BayiID { get; set; }
        public int BayiAdresID { get; set; }
        public int TeslimatAdresID { get; set; }
        public string TeslimatAdresi { get; set; }
        public string SatinalmaNotu { get; set; }
        public int KargoDetayID { get; set; }
        public int SiparisID { get; set; }
        public int KargoFirmaID { get; set; }
        public string KargoReferansNo { get; set; }

        [ForeignKey("KargoFirmaID")]
        public KargoFirmalar kargoFirmaDetay { get; set; }

        [ForeignKey("BayiID")]
        public Bayi bayi { get; set; }

        [ForeignKey("BayiAdresID")]
        public BayiAdresler bayiAdresler { get; set; }

        [ForeignKey("KargoDetayID")]
        public KargoDetay kargoDetayi { get; set; }

        [ForeignKey("SiparisID")]
        public Siparis siparis { get; set; }

    }
}
