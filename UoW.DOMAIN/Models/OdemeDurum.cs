using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("ODEME_DURUM")]
    public class OdemeDurum : BaseEntity
    {
        public int SiparisID { get; set; }
        public DateTime OdemeZaman { get; set; }
        public int OdemeTipiID { get; set; }
        public string OnayKodu { get; set; }
        public bool OdemeTamamMi { get; set; }
        public decimal ToplamOdeme { get; set; }

        [ForeignKey("SiparisID")]
        public Siparis SiparisOdemeDurum { get; set; }
      
        [ForeignKey("OdemeTipiID")]
        public OdemeSekli odemeSekliDurum { get; set; }
    }

}
