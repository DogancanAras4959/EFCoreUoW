using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using UoW.CORE.Enums;
using UoW.SERVICE.Builders.Interfaces;
using UoW.SERVICE.Dtos;
using YurticiKargoWS;

namespace UoW.SERVICE.Builders.Services
{
    public class YurticiCargoService : IYurticiCargoService
    {
        private readonly IConfiguration _configuration;
        private readonly ShippingOrderDispatcherServicesClient _client;
        public YurticiCargoService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new ShippingOrderDispatcherServicesClient();
            _client.Endpoint.Address = new EndpointAddress(new Uri(_configuration["YurticiKargo:Endpoint"]));
        }
        public string CreateCargo(CreateCargoDto createCargoDto)
        {
            var paymentType = GetUsernameAndPassword(createCargoDto.CargoPaymentType);
            createShipment createShipment = new createShipment();
            createShipment.wsUserName = paymentType.Item1;
            createShipment.wsPassword = paymentType.Item2;
            createShipment.userLanguage = "TR";
            createShipment.ShippingOrderVO = new ShippingOrderVO[]{
            new ShippingOrderVO()
            {
                cargoKey = createCargoDto.CargoKey,
                invoiceKey = createCargoDto.InvoiceKey,
                receiverAddress = createCargoDto.ReceiverAddress,
                receiverCustName = createCargoDto.ReceiverName,
                receiverPhone1 = createCargoDto.ReceiverPhoneNumber
            }};
            var response = _client.createShipmentAsync(createShipment).Result;
            var responseCode = response.createShipmentResponse.ShippingOrderResultVO.errCode;
            if (responseCode != "0")
            {
                throw new Exception($"Hata meydana geldi. Hata kodu {responseCode}");
            }
            return "";
        }

        public CargoTrackingInformation GetCargoTrackingInformation(string cargoKey)
        {
            var paymentType = GetUsernameAndPassword(CargoPaymentType.GondericiOdemeli);
            queryShipment queryShipment = new queryShipment();
            queryShipment.wsUserName = paymentType.Item1;
            queryShipment.wsPassword = paymentType.Item2;
            queryShipment.wsLanguage = "TR";
            queryShipment.keyType = 0;
            queryShipment.keys = new string[] { cargoKey };
            queryShipment.addHistoricalData = false;
            queryShipment.onlyTracking = true;
            var response = _client.queryShipmentAsync(queryShipment).Result;
            string trackingUrl = "";
            if (response.queryShipmentResponse.ShippingDeliveryVO.outResult == "0")
            {
                trackingUrl =  response.queryShipmentResponse.ShippingDeliveryVO.shippingDeliveryDetailVO[0].shippingDeliveryItemDetailVO.trackingUrl;
            }
            var result = new YurticiCargoTrackingInformation()
            {
                TrackingUrl = trackingUrl,
                CargoFirmType = Enums.CargoFirmType.Yurtici
            };
            return result;
        }

        private Tuple<string, string> GetUsernameAndPassword(CargoPaymentType cargoPaymentType)
        {
            string paymenType = Enum.GetName(typeof(CargoPaymentType), cargoPaymentType);
            string configUserName = _configuration[$"YurticiKargo:{paymenType}:Username"];
            string configPassword = _configuration[$"YurticiKargo:{paymenType}:Password"];
            return Tuple.Create<string, string>(configUserName, configPassword);
        }
    }
}
