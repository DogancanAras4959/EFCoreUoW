using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("SIPARIS_DURUM")]
    public class SiparisDurum : BaseEntity
    {
        public string Durum { get; set; }
        public bool AktifMi { get; set; }
        public ICollection<Siparis> siparisList { get; set; }

    }
}
