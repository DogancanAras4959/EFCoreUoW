using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("FATURA_TIP")]

    public class FaturaTip : BaseEntity
    {
        public string TipAdi { get; set; }
        public bool Durum { get; set; }
        public DateTime EklenmeTarih { get; set; }

        public int KullaniciID { get; set; }

        [ForeignKey("KullaniciID")]
        public Kullanici KullaniciFatura { get; set; }
        public virtual ICollection<Fatura> faturalar { get; set; }
    }
}
