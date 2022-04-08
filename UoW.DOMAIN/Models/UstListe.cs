using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("UST_URUN_LISTE")]
    public class UstListe : BaseEntity
    {
        public string ListeAdi { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public ICollection<Liste> listelerEleman { get; set; }
        public int BayiID { get; set; }

        [ForeignKey("BayiID")]
        public Bayi bayiListUst { get; set; }
    }
}
