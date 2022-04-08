using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UoW.DOMAIN.Models;

namespace UoW.ADMIN.Models
{
    public class BasketsAdmin
    {
        public int id { get; set; }
        public Urun urun { get; set; }
        public int adet { get; set; }
    }
}
