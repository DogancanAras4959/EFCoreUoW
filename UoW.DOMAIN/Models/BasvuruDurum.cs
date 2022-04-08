using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("BASVURU_DURUM")]
    public class BasvuruDurum : BaseEntity
    {
        public string DurumAdi { get; set; }
        public string Renk { get; set; }
        public virtual ICollection<Basvuru> basvurular { get; set; }
    }
}
