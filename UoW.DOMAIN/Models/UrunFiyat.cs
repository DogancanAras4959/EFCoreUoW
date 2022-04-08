using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("URUN_FIYAT")]
    public class UrunFiyat : BaseEntity
    {
      
        public int AdetBaslangic { get; set; }
        public int AdetBitis { get; set; }

        [DataType("decimal(18,5)")]
        public decimal UrunFiyati
        {
            get;set;
        }

        public int UrunID { get; set; }

        public Urun urun { get; set; }
    }
}
