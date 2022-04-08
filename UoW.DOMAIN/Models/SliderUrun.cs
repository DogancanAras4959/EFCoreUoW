using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("SLIDER_URUN")]
    public class SliderUrun : BaseEntity
    {
        public int UrunID { get; set; }
        public int SliderItemID { get; set; }
        public int SliderBannerID { get; set; }

        [ForeignKey("UrunID")]
        public virtual Urun urunSliderUrun { get; set; }

        [ForeignKey("SliderItemID")]
        public virtual SliderItem sliderItemSliderUrun { get; set; }

        [ForeignKey("SliderBannerID")]
        public virtual SliderBannerItem sliderBannerItemSliderUrun { get; set; }
    }
}
