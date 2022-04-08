using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("SITE_INFO")]
    public class SiteInfo : BaseEntity
    {
        public string Baslik { get; set; }
        public string LinkAdi { get; set; }
        public string Icerik { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public bool AktifMi { get; set; }

        [ForeignKey("kullanici")]
        public int YoneticiID { get; set; }
        public Kullanici kullanici { get; set; }
    }
}
