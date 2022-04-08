using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("BASVURU")]
    public class Basvuru : BaseEntity
    {
        public string BasvuruNo { get; set; }
        public DateTime BasvuruTarih { get; set; }

        [Required(ErrorMessage ="Bayi Adı boş geçilemez")]
        public string BayiAdi { get; set; }
       
        [Required(ErrorMessage = "Bayi Ünvanı boş geçilemez")]
        public string BayiUnvani { get; set; }
      
        [Required(ErrorMessage = "Yetkili Adı boş geçilemez")]
        public string YetkiliAdi { get; set; }

        [Required(ErrorMessage = "Vergi No boş geçilemez")]
        [DataType(DataType.Text, ErrorMessage ="Lütfen Vergi numaranızı doğru bir şekilde giriniz...")]
        public string VergiNo { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telefon Numarası formatı hatalıdır")]
        [Required(ErrorMessage = "Telefon No boş geçilemez")]
        public string TelNo { get; set; }

        public bool EpostaBilgiAlma { get; set; }

        public bool SMSAlma { get; set; }

        public bool BilgilendirmeAlma { get; set; }

        [Required(ErrorMessage = "Üyelik Sözleşmesini kabul etmeden geçemezsiniz")]
        public bool UyelikSozlesmesiOkuduMu { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email Adresi formatı hatalıdır")]
        [Required(ErrorMessage = "Email Adresi boş geçilemez")]
        public string EmailAdresi { get; set; }
        public string basvuruFoto { get; set; }
        public int BasvuruDurumID { get; set; }

        [ForeignKey("BasvuruDurum")]
        public BasvuruDurum basvuruDurum { get; set; }

        public virtual List<BasvuruDosyalar> basvuruDosyalar { get; set; }
    }
}
