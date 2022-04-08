using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.LOG.Models
{
    public class Durumlar : BaseEntity
    {
        public string DurumAdi { get; set; }

        public virtual List<Loglar> loglars { get; set; }
    }
} 
