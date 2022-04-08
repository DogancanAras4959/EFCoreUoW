using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UoW.WEB.Models
{
    public class LoginViewModel
    {
   
        [Required(ErrorMessage ="Kullanıcı Adı zorunludur")]
        public string username { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string password { get; set; }
    }
}
