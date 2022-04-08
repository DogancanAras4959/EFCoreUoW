using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("BAYI_TICARI")]
    public class BayiTicari : BaseEntity
    {
        [Required(ErrorMessage = "Firma Unvanı zorunludur")]
        public string FirmaUnvani { get; set; }

        [Required(ErrorMessage = "Vergi Dairesi zorunludur")]
        public string VergiDairesi { get; set; }

        [Required(ErrorMessage = "Vergi Numarası zorunludur")]
        [DataType(DataType.Text, ErrorMessage ="Vergi Numarası zorunludur. Eksiksiz ve doğru bir şekilde giriniz...")]
        public string VergiNo { get; set; }

        [Required(ErrorMessage = "Firma tipi zorunludur")]
        public string FirmaTipi { get; set; }

        [Required(ErrorMessage = "Web Sitesi zorunludur")]
        public string WebSiteAdresi { get; set; }

        [Required(ErrorMessage = "Adres zorunludur")]
        [DataType(DataType.Text, ErrorMessage ="Adres bölünü zorunludur. Firma adresinizi giriniz...")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Telefon Numarası zorunludur")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Eposta Adresi zorunludur")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Eposta alanı zorunludur. Eposta adresinizi giriniz")]
        public string EpostaAdresi { get; set; }

        [Required(ErrorMessage = "Faks numarası zorunludur")]
        public string Faks { get; set; }
        public int SehirID { get; set; }
        public int IlceID { get; set; }
        public int BayiID { get; set; }
        public int FirmaYetkiliID { get; set; }

        [Required(ErrorMessage = "Posta Kodu zorunludur")]
        public string PostaKodu { get; set; }

        public string MuhasebeMutabakatTel { get; set; }

        public string MuhasebeMutabakatEmail { get; set; }

        [Required(ErrorMessage = "IBAN TL zorunludur")]
        public string IBanNoTL { get; set; }

        [Required(ErrorMessage = "IBAN USD zorunludur")]
        public string IBanNoUSD { get; set; }

        [Required(ErrorMessage = "IBan EUR zorunludur")]
        public string IBanNoEUR { get; set; }
        public virtual Yetkililer firmaYetkili { get; set; }
        public virtual Bayi bayiBayiTicari { get; set; }
        public virtual ICollection<Fatura> faturaListBayi{ get; set; }
    }
}
