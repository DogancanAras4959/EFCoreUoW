using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("SLIDER_ITEM")]
    public class SliderItem : BaseEntity
    {
        [Required(ErrorMessage = "Eleman Adı zorunludur")]
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public string ItemDesc { get; set; }
        public bool IsTıtle { get; set; }
        public byte OrderBy { get; set; }
        public int SliderID { get; set; }
        public int KampanyaKodu { get; set; }

        [ForeignKey("SliderID")]
        public Slider sliderItemSlider { get; set; }

        public virtual ICollection<SliderUrun> sliderItemSliderUrunList { get; set; }

    }
}
