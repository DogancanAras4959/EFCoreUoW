using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("KAMPANYA_KOD")]
    public class KampanyaKoduSliderBanner : BaseEntity
    {
        public string Kod { get; set; }        
       
    }
}
