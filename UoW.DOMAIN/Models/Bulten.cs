using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("BULTEN_UYELIKLERI")]
    public class Bulten : BaseEntity
    {
        public int YetkiliID { get; set; }

        [Required(ErrorMessage = "Eposta Adresi zorunludur")]
        public string EmailAdresi { get; set; }

        [Required(ErrorMessage = "Telefon Numarası zorunludur")]
        public string TelefonNo { get; set; }

        [Required(ErrorMessage = "İsim Soyisim zorunludur")]
        public string AdinizSoyadiniz { get; set; }
        public bool Durum { get; set; }

        public Yetkililer yetkili { get; set; }
    }
}
