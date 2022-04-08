using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("BAYI_ADRESLER")]
    public class BayiAdresler : BaseEntity
    {

        [Required(ErrorMessage = "Adres zorunludur")]
        public string AdresAdi { get; set; }

        public string TeslimatAdresAdi { get; set; }
        public int SehirID { get; set; }
        public int IlceID { get; set; }
        public string SiparisNotu { get; set; }
        public string Adres { get; set; }
        public int BayiID { get; set; }

        [ForeignKey("BayiID")]
        public Bayi bayiAdres { get; set; }

        [ForeignKey("SehirID")]
        public Sehir sehirAdres { get; set; }

        [ForeignKey("IlceID")]
        public Ilce ilceAdres { get; set; }
        public virtual ICollection<Fatura> faturaBayiAdres { get; set; }
        public virtual ICollection<Siparis> siparisBayiAdres { get; set; }
        public virtual ICollection<Kargo> kargoAdres { get; set; }
    }
}
