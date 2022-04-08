using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("YETKILILER")]
    public class Yetkililer : BaseEntity
    {
        [Required(ErrorMessage = "Yetkili Adı zorunludur")]
        public string YetkiliAdi { get; set; }

        [Required(ErrorMessage = "Telefon Numarası zorunludur")]
        public string TelNo { get; set; }

        [Required(ErrorMessage = "Email Adresi zorunludur")]
        public string Email { get; set; }
        public int BayiID { get; set; }
        public bool Durum { get; set; }
        public DateTime EklenmeTarih { get; set; }
        public DateTime GuncellenmeTarih { get; set; }
        public string YetkiliFoto { get; set; }
        public virtual Bayi bayiYetkilisi { get; set; }
        public virtual ICollection<BayiTicari> ticariList { get; set; }
        public virtual ICollection<Bulten> bultenler { get; set; }
    }
}
