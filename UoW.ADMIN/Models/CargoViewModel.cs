using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UoW.ADMIN.Models
{
    public class CargoViewModel
    {
        public string BayiAdi { get; set; }
        public string EmailAdresi { get; set; }
        public string TelefonNo { get; set; }
        public string Adres { get; set; }
        public int SehirID { get; set; }
        public int IlceID { get; set; }
        public string SatinAlmaNotu { get; set; }
        public int SiparisID { get; set; }
    }
}
