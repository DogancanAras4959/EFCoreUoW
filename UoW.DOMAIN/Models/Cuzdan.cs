using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("CUZDAN")]
    public class Cuzdan : BaseEntity
    {
        [DataType("decimal(18,2")]
        public decimal Bakiye { get; set; }
        public int BayiID { get; set; }
        public DateTime OlusturulmaTarih { get; set; }

        [Required(ErrorMessage = "Cüzdan Numarası zorunludur")]
        public string CuzdanNo { get; set; }

        [Required(ErrorMessage = "Cüzdan Tipi zorunludur")]
        public string CuzdanTipi { get; set; }

        [ForeignKey("BayiID")]
        public Bayi bayi { get; set; }

        public virtual ICollection<CuzdanIslemler> cuzdanIslemlers { get; set; }
    }
}
