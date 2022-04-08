using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using UoW.CORE.Enums;
using UoW.SERVICE.Builders.Enums;
using UoW.SERVICE.Builders.Interfaces;
using YurticiKargoWS;

namespace UoW.SERVICE.Builders
{
    public class CargoServiceBuilder : ICargoServiceBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        public CargoServiceBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IBaseCargoService CreateCargoService(CargoFirmType cargoFirmType)
        {
            switch (cargoFirmType)
            {
                case CargoFirmType.Yurtici:
                    return _serviceProvider.GetRequiredService<IYurticiCargoService>();
                case CargoFirmType.Surat:
                    return _serviceProvider.GetRequiredService<ISuratCargoService>();
                default:
                    throw new Exception("Not found appropriate cargo firm type");
            }
        }
    }
}
