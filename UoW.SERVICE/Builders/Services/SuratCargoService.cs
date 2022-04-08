using Microsoft.Extensions.Configuration;
using SuratKargoWS;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.SERVICE.Builders.CargoSettings;
using UoW.SERVICE.Builders.Interfaces;
using UoW.SERVICE.Dtos;

namespace UoW.SERVICE.Builders.Services
{
    public class SuratCargoService : ISuratCargoService
    {
        private readonly servicesSoapClient _client;
        private readonly SuratCargoSettings _suratCargoSettings;
        public SuratCargoService(SuratCargoSettings suratCargoSettings)
        {
            _suratCargoSettings = suratCargoSettings;
            _client = new servicesSoapClient(servicesSoapClient.EndpointConfiguration.servicesSoap12,
                suratCargoSettings.Endpoint);
        }
        public string CreateCargo(CreateCargoDto createCargoDto)
        {
            var gonderi = new GonderiKargoModel()
            {
                KisiKurum = createCargoDto.ReceiverName,
                AliciAdresi = createCargoDto.ReceiverAddress,
                Adet = 1,
                Il = createCargoDto.CityName,
                Ilce = createCargoDto.TownName,
                TelefonCep = createCargoDto.ReceiverPhoneNumber,
                KargoTuru = 3,
                Odemetipi = 1,
                OzelKargoTakipNo = createCargoDto.CargoKey,
            };
            var response = _client.GonderiyiKargoyaGonderAsync(_suratCargoSettings.CariNo,
                _suratCargoSettings.Password, gonderi).Result;  
            if(response == "Tamam")
            {
                return createCargoDto.CargoKey;
            }
            else
            {
                throw new Exception(response);
            }
            
        }

        public CargoTrackingInformation GetCargoTrackingInformation(string cargoKey)
        {
            var response = _client.WebSiparisKodundanKargoTeslimatBilgisiAsync(_suratCargoSettings.CariNo, cargoKey, _suratCargoSettings.Password).Result;
            var result = new SuratCargoTrackingInformation()
            {
                LastMovementDate = response?.TeslimatBilgisi?.SonHareketTarihi,
                LastMovementExplanation = response?.TeslimatBilgisi?.SonHareket,
                LastLocation = response?.TeslimatBilgisi?.SonBulunduguYer,
                LastStatus = response?.TeslimatBilgisi?.SonDurum,
                DeliveryDate = response.TeslimatBilgisi?.TeslimatTarihi,
                DeliveryReceiver = response?.TeslimatBilgisi?.TeslimAlan,
                DeliveryExplanation = response?.TeslimatBilgisi?.TeslimatAciklamasi,
                CargoFirmType = Enums.CargoFirmType.Surat
            };
            return result;
        }
    }
}
