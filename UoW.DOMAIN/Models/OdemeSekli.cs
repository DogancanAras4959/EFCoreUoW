using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("ODEME_TURU")]
    public class OdemeSekli : BaseEntity
    {
        public string Odeme { get; set; }
        public virtual ICollection<Bayi> listBayi { get; set; }
        public virtual ICollection<Siparis> listSiparis { get; set; }

        public virtual ICollection<OdemeDurum> listOdemeDurum { get; set; }

        public virtual ICollection<CuzdanIslemler> listCuzdenIslemler { get; set; }
    }
}
