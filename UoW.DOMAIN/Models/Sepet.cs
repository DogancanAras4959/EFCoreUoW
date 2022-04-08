using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("SEPET")]

    public class Sepet : BaseEntity
    {
        public int UrunID { get; set; }
        public int SiparisID { get; set; }
        public int Adet { get; set; }

        [DataType("decimal(18,2")]
        public decimal BirimFiyat { get; set; }

        [DataType("decimal(18,2")]
        public decimal ToplamSatır { get; set; }
     
        [ForeignKey("UrunID")]
        public Urun UrunSepet { get; set; }

        [ForeignKey("SiparisID")]
        public Siparis siparisSepet  { get; set; }

        public bool urunSepetteMi { get; set; }

        public int BayiID { get; set; }

        [ForeignKey("BayiID")]
        public Bayi BayiSepet { get; set; }

    }
}
