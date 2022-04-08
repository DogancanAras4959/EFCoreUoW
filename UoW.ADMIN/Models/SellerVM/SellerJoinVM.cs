using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UoW.ADMIN.Models.BayiVM
{
    public class SellerJoinVM
    {

        [Required(ErrorMessage="Bayi Adı zorunludur")]
        public string BayiAdi { get; set; }

        [Required(ErrorMessage = "Bayi Unvanı zorunludur")]
        public string BayiUnvani { get; set; }

        [Required(ErrorMessage = "Telefon Numarası zorunludur")]
        public string TelefonNo { get; set; }

        [Required(ErrorMessage = "Yetkili Adı zorunludur")]
        public string YetkiliAdi { get; set; }

        [Required(ErrorMessage = "Eposta Adresi zorunludur")]
        public string EmailAdresi { get; set; }
        public string Logo { get; set; }
    }
}
