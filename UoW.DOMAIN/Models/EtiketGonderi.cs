using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UoW.DOMAIN.Models;

namespace UoW.DOMAIN.Models
{
    [Table("ETIKET_GONDERI")]
    public class EtiketGonderi : BaseEntity
    {
        public int EtiketID { get; set; }
        public int GonderiID { get; set; }

        public virtual Etiketler etiketler { get; set; }
        public virtual Urun urunler { get; set; }
    }
}
