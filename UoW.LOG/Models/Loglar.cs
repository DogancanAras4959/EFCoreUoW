using System;
using System.Collections.Generic;
using System.Text;

namespace UoW.LOG.Models
{
    public class Loglar : BaseEntity
    {
        public int YoneticiID { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int IslemID { get; set; }
        public int DurumID { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime Saat { get; set; }

        public Islemler islemler { get; set; }
        public Durumlar durumlar { get; set; }
        public Yonetici yonetici { get; set; }
    }
}
