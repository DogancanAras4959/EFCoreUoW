using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("BASVURU_AYAR")]
    public class BasvuruAyar : BaseEntity
    {
        public int BasvuruLimitGunluk { get; set; }
        public bool BasvuruAl { get; set; }
        public int YoneticiID { get; set; }
        public bool SecilenAyar { get; set; }
        public DateTime GuncellenmeTarih { get; set; }

        [ForeignKey("YoneticiID")]
        public Kullanici kullaniciBasvuruAyar { get; set; }
        
    }
}
