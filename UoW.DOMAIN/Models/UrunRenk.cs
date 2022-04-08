using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("URUN_RENK")]
    public class UrunRenk : BaseEntity
    {
        [ForeignKey("renk")]
        public int RenkID { get; set; }

        [ForeignKey("urun")]
        public int UrunID { get; set; }
        public string Resim { get; set; }
        public int Adet { get; set; }

        public Urun urun { get; set; }
        public Renk renk { get; set; }
    }
}
