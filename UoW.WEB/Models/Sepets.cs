using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.DOMAIN.Models;

namespace UoW.WEB.Models
{
    public class Sepets
    {
        public int id { get; set; }
        public Urun urun { get; set; }
        public int adet { get; set; }
        public int iskonto { get; set; }
 
        public string fiyatcek;
        public decimal fiyat
        {
            get
            {
                return Convert.ToDecimal(fiyatcek);
            }
            set { fiyatcek = value.ToString(); }
        }
        public decimal AraToplam { get; set; }
        public decimal Indirim { get; set; }
        public decimal KargoBedeli { get; set; }
        public decimal ToplamTutar { get; set; }

    }
}
