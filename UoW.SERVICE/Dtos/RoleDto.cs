using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UoW.SERVICE.Dtos
{
    public class RoleDto : BaseEntityDto
    {
        [Required(ErrorMessage = "Rol adı zorunludur")]
        public string RolAdi { get; set; }
        public bool Durum { get; set; }

        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public List<YetkiDto> Yetkiler { get; set; }
    }
}
