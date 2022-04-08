using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("URUN_STOKLARI")]
    public class UrunStok : BaseEntity
    {
        public int BayiID { get; set; }
        public int UrunID { get; set; }
        public int Adet { get; set; }
        public bool Durum { get; set; }

        [ForeignKey("BayiID")]
        public Bayi bayiStok { get; set; }

        [ForeignKey("UrunID")]
        public Urun bayiUrun { get; set; }
    }
}
