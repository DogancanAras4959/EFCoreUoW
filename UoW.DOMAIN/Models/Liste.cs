using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("URUN_LISTE")]
    public class Liste : BaseEntity
    {
        public int UrunID { get; set; }
        public int BayiID { get; set; }
        public int ListeUstID { get; set; }
        public bool aktifMi { get; set; }
        public DateTime EklenmeTarih { get; set; }

        [ForeignKey("Urun")]
        public Urun urunListe { get; set; }

        [ForeignKey("Bayi")]
        public Bayi bayiList { get; set; }

        [ForeignKey("ListeUstID")]
        public UstListe listeUst { get; set; }
    }
}
