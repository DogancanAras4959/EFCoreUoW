using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UoW.DOMAIN.Models;

namespace UoW.ADMIN.Models
{
    public class UserInsertViewModel
    {
        [Required(ErrorMessage="Kullanıcı Adı zorunludur")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur")]
        public string SifreTekrar { get; set; }
        public bool Durum { get; set; } = true;
        public DateTime EklenmeTarih { get; set; } = DateTime.Now;
        public DateTime GuncellenmeTarih { get; set; } = DateTime.Now;
        public string ProfilResim { get; set; }
        public int YetkiID { get; set; }
        public virtual Rol yetkiKullanici { get; set; }

    }
}
