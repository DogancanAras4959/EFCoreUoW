using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("BASVURU_DOSYALAR")]
    public class BasvuruDosyalar : BaseEntity
    {
        public string DosyaAdi { get; set; }
        public string DosyaTuru { get; set; }
        public string DosyaBoyutu { get; set; }
        public int BasvuruID { get; set; }

        [ForeignKey("BasvuruID")]
        public Basvuru basvuru { get; set; }
    }
}
