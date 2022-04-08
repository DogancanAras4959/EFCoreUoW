using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("URUN_BEGENI")]

    public class Begeni : BaseEntity
    {
        public int UrunID { get; set; }
        public int BayiID { get; set; }
        public bool aktifMi { get; set; }
        public DateTime EklenmeTarih { get; set; }

        [ForeignKey("Urun")]
        public Urun urunBegeni { get; set; }

        [ForeignKey("Bayi")]
        public Bayi bayiBegeni { get; set; }
    }
}
