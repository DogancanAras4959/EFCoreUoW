using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.DOMAIN.Models
{
    public class KrediKartBIN : BaseEntity
    {
        public int BIN { get; set; }
        public int BankaKodu { get; set; }
        public string BankaAdi { get; set; }
        public string Tipi { get; set; }
        public string Kademe { get; set; }
        public string Sanal { get; set; }
        public string onOdeme { get; set; }
    }
}
