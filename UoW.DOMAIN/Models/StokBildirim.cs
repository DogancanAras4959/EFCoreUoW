using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using UoW.CORE.Enums;

namespace UoW.DOMAIN.Models
{
    [Table("STOK_BILDIRIM")]
    public class StokBildirim : BaseEntity
    {
        public int BayiId { get; set; }
        public int UrunId { get; set; }
        public StokBildirimDurum Durum { get; set; }
        public DateTime BildirimTarihi { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public Bayi Bayi { get; set; }
        public Urun urun { get; set; }
    }
}
