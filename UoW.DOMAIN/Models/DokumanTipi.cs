using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("DOKUMAN_TIPI")]
    public class DokumanTipi : BaseEntity
    {
        public string TipAdi { get; set; }
        public bool Durum { get; set; }
        public string Pozisyon { get; set; }
        public ICollection<Dokuman> dokumans { get; set; }
    }
}
