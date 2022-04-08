using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("RENK")]
    public class Renk : BaseEntity
    {
        public string RenkAdi { get; set; }
        public string RenkKodu { get; set; }
        public ICollection<UrunRenk> urunRenkRenk { get; set; }
    }
}
