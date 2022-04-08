using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UoW.DOMAIN.Models;
using Xunit;

namespace UoW.ADMIN.Models.BayiVM
{
    public class SellerEditVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Bayi Adı zorunludur")]
        public string BayiAdi { get; set; }

        [Required(ErrorMessage = "Üye Numarası zorunludur")]
        public string UyeNo { get; set; }
        public int BayiGrubuID { get; set; }
        public int OdemeID { get; set; }

        [Required(ErrorMessage = "Risk Limiti zorunludur")]
        public decimal RiskLimiti { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı zorunludur")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur")]
        public string SifreTekrar { get; set; }
        public string Logo { get; set; }
        public MusteriGrubu musteriGrubu { get; set; }
        public OdemeSekli odeme { get; set; }
    }
}
