using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("DOKUMAN")]
    public class Dokuman : BaseEntity
    {
        public string KatalogAdi { get; set; }

        [ForeignKey("kullanici")]
        public int YoneticiID { get; set; }
        public string Onizleme { get; set; }
        public string DosyaYolu { get; set; }
        public bool Durum { get; set; }

        [ForeignKey("dokuman")]
        public int DokumanTipi { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public Kullanici kullanici { get; set; }
        public DokumanTipi dokuman { get; set; }
    }
}
