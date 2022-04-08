using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("ROL_YETKI")]
    public class RolYetki : BaseEntity
    {
        [ForeignKey("Rol")]
        public int RolID { get; set; }
        [ForeignKey("Yetki")]
        public int YetkiID { get; set; }     
        public Rol Rol { get; set; }
        public Yetki Yetki { get; set; }
    }
}
