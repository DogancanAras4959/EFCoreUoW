using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UoW.DOMAIN.Models;

namespace UoW.ADMIN.Models
{
    public class UserEditViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Kullanıcı adı boş bırakılamaz")]
        public string KullaniciAdi { get; set; }
        public string ProfilResim { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı boş bırakılamaz")]
        public string SifreTekrar { get; set; }
        public int YetkiID { get; set; }
        public virtual Rol yetkiKullanici { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public List<Rol> yetkiList { get; set; }
    }
}
