using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("FATURA")]

    public class Fatura : BaseEntity
    {
        public string FaturaNo { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public int FaturaTipiID { get; set; }
        public int BayiID { get; set; }
        public int SiparisID { get; set; }
        public int BayiAdresID { get; set; }

        [DataType("decimal(18,2)")]
        public decimal ToplamFiyat { get; set; }

        [ForeignKey("FaturaTipiID")]
        public FaturaTip faturaTipi { get; set; }

        [ForeignKey("SiparisID")]
        public Siparis siparisFatura { get; set; }

        [ForeignKey("BayiID")]
        public Bayi bayiFatura { get; set; }

        [ForeignKey("BayiAdresID")]
        public BayiAdresler bayiAdresler { get; set; }
    } 
}
