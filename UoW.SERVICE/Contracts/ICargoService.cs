using System;
using System.Collections.Generic;
using System.Text;
using UoW.DOMAIN.Models;
using UoW.SERVICE.Dtos;

namespace UoW.SERVICE.Contracts
{
    public interface ICargoService
    {
        public string GenerateCargoKey(CargoDto cargoDto);
        public CargoTrackingInformation GetCargoTrackingInformation(string cargoKey);
    }
}
