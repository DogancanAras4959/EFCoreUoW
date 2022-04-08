using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("SIPARIS")]
    public class Siparis : BaseEntity
    {
        public string SiparisNo { get; set; }
        public DateTime SiparisTarih { get; set; }
        public DateTime HazirlanmaTarih { get; set; }
        public int BayiID { get; set; }

        public int BayiAdresID { get; set; }
        public int TeslimatAdresID { get; set; }

        [DataType("decimal(18,2)")]
        public decimal ToplamFiyat { get; set; }

        public string ReferansNo { get; set; }
        public decimal Indirim { get; set; }
        public decimal KargoBedeli { get; set; }
        public bool Durum { get; set; }
        public int OdemeSekliID { get; set; }
        public string TeslimatAdres { get; set; }
        public string SiparisNotu { get; set; }

        [ForeignKey("siparisDurumSiparis")]
        public int SiparisDurumID { get; set; }

        [ForeignKey("BayiID")]
        public Bayi bayiSiparis { get; set; }

        [ForeignKey("BayiAdresID")]
        public BayiAdresler bayiAdresleri { get; set; }

        [ForeignKey("OdemeSekliID")]
        public OdemeSekli odemeSekliSiparis { get; set; }
        public virtual ICollection<Fatura> faturaSiparis { get; set; }
        public virtual ICollection<Sepet> sepetSiparis { get; set; }
        public virtual ICollection<Kargo> kargoSiparis { get; set; }
        public virtual ICollection<OzelTeklif> ozelTekliflerSepetList { get; set; }
        public SiparisDurum siparisDurumSiparis { get; set; }
    }
}
