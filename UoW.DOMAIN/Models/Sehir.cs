using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("SEHIR")]
    public class Sehir : BaseEntity
    {
        public string SehirAdi { get; set; }
        public virtual ICollection<Ilce> ilceler { get; set; }
        public virtual ICollection<BayiTicari> bayiler { get; set; }
        public virtual ICollection<BayiAdresler> sehirAdresler { get; set; }

    }
}
