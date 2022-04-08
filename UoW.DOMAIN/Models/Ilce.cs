using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("ILCE")]
    public class Ilce : BaseEntity
    {
        public string IlceAdi { get; set; }
        public int SehirID { get; set; }
        public virtual Sehir sehirIlce { get; set; }
        public virtual ICollection<BayiTicari> bayiler { get; set; }

        public virtual ICollection<BayiAdresler> ilceAdresler { get; set; }
    }
}
