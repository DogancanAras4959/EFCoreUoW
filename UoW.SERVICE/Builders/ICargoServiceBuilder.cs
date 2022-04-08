using System;
using System.Collections.Generic;
using System.Text;
using UoW.SERVICE.Builders.Enums;
using UoW.SERVICE.Builders.Interfaces;

namespace UoW.SERVICE.Builders
{
    public interface ICargoServiceBuilder
    {
        public IBaseCargoService CreateCargoService(CargoFirmType cargoFirmType);
    }
}
