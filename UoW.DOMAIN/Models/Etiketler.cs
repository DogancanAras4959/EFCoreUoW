using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("ETIKETLER")]
    public class Etiketler : BaseEntity
    {
        public string EtiketAdi { get; set; }

        public virtual ICollection<EtiketGonderi> etiketlerGonderi  { get; set; }
    }
}
