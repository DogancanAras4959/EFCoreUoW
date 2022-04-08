using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UoW.DOMAIN.Models;

namespace UoW.ADMIN.Models.BayiVM
{
    public class SellerLoginVM
    {

        public int ID { get; set; }

        [Required(ErrorMessage ="Kullanıcı Adı zorunludur")]
        public string username { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string password { get; set; }

        public Bayi bayi;
    }
}
