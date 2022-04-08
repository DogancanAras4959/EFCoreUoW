using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UoW.WEB.Models
{
    public class CargoViewModels
    {
        public string BayiAdi { get; set; }
        public string EmailAdresi { get; set; }
        public string TelefonNo { get; set; }
        public string Adres { get; set; }
        public string TeslimatAdres { get; set; }
        public int SehirID { get; set; }
        public int IlceID { get; set; }
        public string BuyProductmaNotu { get; set; }
        public int SiparisID { get; set; }
        public int OdemeTuruID { get; set; }
        public DateTime TeslimatTarih { get; set; }
        public int SeciliKargoFirmaId { get; set; }
    }
}
