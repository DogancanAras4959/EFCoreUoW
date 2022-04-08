using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("SLIDER_BANNER_ITEM")]
    public class SliderBannerItem : BaseEntity
    {
        [Required(ErrorMessage = "Banner Adı zorunludur")]
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public string ItemDesc { get; set; }
        public byte OrderBy { get; set; }
        public int SliderID { get; set; }
        public int KampanyaKodu { get; set; }

        [ForeignKey("SliderID")]
        public Slider sliderBannerItemSlider { get; set; }

        public virtual ICollection<SliderUrun> sliderBannerItemSliderUrun { get; set; }

    }
}
