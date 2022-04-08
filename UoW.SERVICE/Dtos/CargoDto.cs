using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.SERVICE.Dtos
{
    public class CargoDto : BaseEntityDto
    {
        public int BayiID { get; set; }
        public int BayiAdresID { get; set; }
        public int TeslimatAdresID { get; set; }
        public string TeslimatAdresi { get; set; }
        public string SatinalmaNotu { get; set; }
        public int KargoDetayID { get; set; }
        public int SiparisID { get; set; }
        public int KargoFirmaID { get; set; }
        public string KargoReferansNo { get; set; }
        public string FaturaNo { get; set; }
        public string AliciAdi { get; set; }
        public string AliciTelefonNumarasi { get; set; }
        public int SehirId { get; set; }
        public int IlceId { get; set; }
    }
}
