using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UoW.ADMIN.Models
{
    public class BasketViewModelAdmin
    {
        public BasketViewModelAdmin()
        {
            this.Urunler = new Dictionary<int, int>();
        }
        public Dictionary<int, int> Urunler { get; set; }
    }
}
