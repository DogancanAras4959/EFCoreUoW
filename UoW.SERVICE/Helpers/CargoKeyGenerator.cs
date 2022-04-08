using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UoW.SERVICE.Helpers
{
    public static class CargoKeyGenerator
    {
        private static Random random = new Random();
        public static string Generate()
        {
            string key = DateTime.Now.Ticks.ToString();
            return key;
        }
    }
}
