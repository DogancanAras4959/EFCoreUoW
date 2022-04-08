using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("KARGO_DETAY")]
    public class KargoDetay : BaseEntity
    {
        public DateTime HazirlanmaTarih { get; set; }
        public DateTime TeslimTarih { get; set; }
        public DateTime EklenmEtarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public bool TeslimEdildiMi { get; set; }
        public virtual ICollection<Kargo> kargoList { get; set; }

    }
}
