using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UoW.DOMAIN.Models
{
    [Table("SLIDER")]
    public class Slider : BaseEntity
    {
        [Required(ErrorMessage = "Slider adı zorunludur")]
        public string SliderName { get; set; }

        public string SliderDesc { get; set; }
        public string Thumbnail { get; set; }
        public int YoneticiID { get; set; }
        public bool IsActive { get; set; }
        public bool IsUse { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        
        [ForeignKey("YoneticiID")]
        public Kullanici sliderKullanici { get; set; }
    
        public virtual ICollection<SliderItem> sliderItems { get; set; }
        public virtual ICollection<SliderBannerItem> sliderBannerItems { get; set; }
    }
}
