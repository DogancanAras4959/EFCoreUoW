using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UoW.CORE.Interface;
using UoW.DOMAIN.Models;

namespace UoW.ADMIN.Models
{
    public class LoginViewModel
    {
     
        [Required(ErrorMessage ="Kullanıcı Adı zorunludur")]
        public string username { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string password { get; set; }
    }
}
