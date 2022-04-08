using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UoW.CORE.Enums;

namespace UoW.DOMAIN.Models
{
    [Table("OZEL_TEKLIF")]
    public class OzelTeklif : BaseEntity
    {
        public int SiparisID { get; set; }
        public int BayiID { get; set; }
        public bool Durum { get; set; }
        public OzelTeklifDurum TeklifDurum { get; set; }
        public DateTime TeklifTarih { get; set; }

        [ForeignKey("SiparisID")]
        public Siparis siparisOzelTeklif { get; set; }
        
        [ForeignKey("BayiID")]
        public Bayi bayiOzelTeklif { get; set; }
    }
}
