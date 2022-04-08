using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("YETKI")]
    public class Yetki : BaseEntity
    {
        public string YetkiAdi { get; set; }
        public string YetkiKodu { get; set; }
        
    }
}
