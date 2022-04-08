using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UoW.ADMIN.Models
{
    public class ProductViewModel
    {

        public int ID { get; set; }
        public string WebServisKodu { get; set; }
        public string TedarikciKodu { get; set; }
        public string KategoriKodu { get; set; }
        public string UrunAdi { get; set; }
        public decimal Fiyat { get; set; }
        public string Barkod { get; set; }
        public int Adet { get; set; }
        public decimal KDV { get; set; }
        public decimal KDVFiyat { get; set; }
        public string FiyatBirimi { get; set; }
        public string StokBirimi { get; set; }
        public string Marka { get; set; }
        public string Kategori { get; set; }
        public string Aciklama { get; set; }
        public double En { get; set; }
        public double Boy { get; set; }
        public double Derinlik { get; set; }
        public double Agirlik { get; set; }
        public double Desi { get; set; }
        public string Resim { get; set; }
        public string UrunNo { get; set; }
        public decimal IndirimliFiyat { get; set; }
        public decimal IndirimOrani { get; set; }
    
    }
}
