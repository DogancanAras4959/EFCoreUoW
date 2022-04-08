using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("ROL")]
    public class Rol : BaseEntity
    {
        [Required(ErrorMessage ="Rol adı zorunludur")]
        public string RolAdi { get; set; }
        public bool Durum { get; set; }

        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public virtual ICollection<Kullanici> kullanicis { get; set; }
        public virtual ICollection<RolYetki> rolYetkis { get; set; }
    }
}
