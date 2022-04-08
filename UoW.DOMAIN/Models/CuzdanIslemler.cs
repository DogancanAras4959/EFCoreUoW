using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UoW.CORE.Enums;

namespace UoW.DOMAIN.Models
{
    [Table("CUZDAN_ISLEMLER")]
    public class CuzdanIslemler : BaseEntity
    {
        public string IslemNo { get; set; }
        public DateTime IslemTarihi { get; set; }
        public int CuzdanID { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public IslemDurum IslemDurum { get; set; }
        public int OdemeSekliID { get; set; }

        [DataType("decimal(18,2")]
        public decimal IslemTutari { get; set; }

        [ForeignKey("CuzdanID")]
        public Cuzdan cuzdanHesabi { get; set; }

        [ForeignKey("OdemeSekliID")]
        public OdemeSekli odemeSekliCuzdan { get; set; }
    }
}
