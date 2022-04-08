using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.LOG.Models
{
    public class Yonetici : BaseEntity
    {
        public string KullaniciAdi { get; set; }
        public virtual List<Loglar> loglars { get; set; }
    }
}
