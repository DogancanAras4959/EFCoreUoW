using System;
using System.Collections.Generic;
using System.Text;
using UoW.CORE.Enums;

namespace UoW.SERVICE.Dtos
{
    public class CreateCargoDto
    {
        public string CityName { get; set; }
        public string TownName { get; set; }
        public CargoPaymentType CargoPaymentType { get; set; }
        public string CargoKey { get; set; }
        public string InvoiceKey { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverPhoneNumber { get; set; }
    }
}
