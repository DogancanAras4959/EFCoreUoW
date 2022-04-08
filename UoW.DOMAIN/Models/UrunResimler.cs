using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("URUN_RESİMLER")]
    public class UrunResimler : BaseEntity
    {
        public string ResimYol { get; set; }
        public int UrunID { get; set; }
        public virtual Urun urun { get; set; }
    }
}
