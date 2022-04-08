using System;
using System.Collections.Generic;
using System.Text;
using UoW.SERVICE.Builders.Enums;

namespace UoW.SERVICE.Dtos
{
    public class CargoTrackingInformation
    {
        public CargoFirmType CargoFirmType { get; set; }
    }

    public class SuratCargoTrackingInformation : CargoTrackingInformation
    {
        public string DeliveryReceiver { get; set; }
        public string DeliveryExplanation { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string LastMovementExplanation { get; set; }
        public DateTime? LastMovementDate { get; set; }
        public string LastStatus { get; set; }
        public string LastLocation { get; set; }
    }

    public class YurticiCargoTrackingInformation : CargoTrackingInformation
    {
        public string TrackingUrl { get; set; }
    }
}
