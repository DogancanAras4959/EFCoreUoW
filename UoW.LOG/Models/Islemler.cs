using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.LOG.Models
{
    public class Islemler : BaseEntity
    {
        public string IslemAdi { get; set; }
        public DateTime Eklenme { get; set; }
        public virtual List<Loglar> loglars { get; set; }
    }
}
