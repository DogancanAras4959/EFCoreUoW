using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using UoW.CORE.Enums;
using UoW.CORE.Interface;
using UoW.DOMAIN.Models;
using UoW.SERVICE.Builders;
using UoW.SERVICE.Builders.Enums;
using UoW.SERVICE.Contracts;
using UoW.SERVICE.Dtos;
using UoW.SERVICE.Helpers;
using YurticiKargoWS;

namespace UoW.SERVICE.Services
{
    public class CargoService : ICargoService
    {
        private readonly IConfiguration _configuration;
        private readonly ICargoServiceBuilder _cargoServiceBuilder;
        private readonly IRepository<KargoFirmalar> _repository;
        private readonly IRepository<Sehir> _sehirRepository;
        private readonly IRepository<Kargo> _kargoRepository;
        public CargoService(IConfiguration configuration, ICargoServiceBuilder cargoServiceBuilder,
            IRepository<KargoFirmalar> repository, IRepository<Sehir> sehirRepository,
            IRepository<Kargo> kargoRepository)
        {
            _configuration = configuration;
            _cargoServiceBuilder = cargoServiceBuilder;
            _repository = repository;
            _sehirRepository = sehirRepository;
            _kargoRepository = kargoRepository;
        }

        public string GenerateCargoKey(CargoDto cargoDto)
        {
            var city = _sehirRepository.Include("ilceler").FirstOrDefault(x => x.ID == cargoDto.SehirId);
            var cargoFirm = _repository.GetById(cargoDto.KargoFirmaID).Result;
            string cargoKey = CargoKeyGenerator.Generate();
            var createCargoDto = new CreateCargoDto()
            {
                CargoPaymentType = CargoPaymentType.GondericiOdemeli,
                CargoKey = cargoKey,
                InvoiceKey = cargoDto.FaturaNo,
                ReceiverAddress = cargoDto.TeslimatAdresi,
                ReceiverName = cargoDto.AliciAdi,
                ReceiverPhoneNumber = cargoDto.AliciTelefonNumarasi,
                CityName = city.SehirAdi,
                TownName = city.ilceler.FirstOrDefault(x => x.ID == cargoDto.IlceId).IlceAdi
            };
            var cargoFirmType = CargoFirmType.Yurtici;
            if (cargoFirm.KargoAdi.ToLower().Contains("s")
                && cargoFirm.KargoAdi.ToLower().Contains("r")
                && cargoFirm.KargoAdi.ToLower().Contains("t")
                && cargoFirm.KargoAdi.ToLower().Contains("u"))
            {
                cargoFirmType = CargoFirmType.Surat;
            }
            var cargoWebService = _cargoServiceBuilder.CreateCargoService(cargoFirmType);
            cargoWebService.CreateCargo(createCargoDto);
            return cargoKey;
        }

        public CargoTrackingInformation GetCargoTrackingInformation(string cargoKey)
        {
            var cargo = _kargoRepository.FirstOrDefault(x => x.KargoReferansNo == cargoKey);
            var cargoFirm = _repository.GetById(cargo.KargoFirmaID).Result;
            var cargoFirmType = CargoFirmType.Yurtici;
            if (cargoFirm.KargoAdi.ToLower().Contains("s")
                && cargoFirm.KargoAdi.ToLower().Contains("r")
                && cargoFirm.KargoAdi.ToLower().Contains("t")
                && cargoFirm.KargoAdi.Replace("Ü","U").Replace("ü","u").ToLower().Contains("u"))
            {
                cargoFirmType = CargoFirmType.Surat;
            }
            var cargoWebService = _cargoServiceBuilder.CreateCargoService(cargoFirmType);
            var result = cargoWebService.GetCargoTrackingInformation(cargoKey);
            return result;
        }
    }
}
