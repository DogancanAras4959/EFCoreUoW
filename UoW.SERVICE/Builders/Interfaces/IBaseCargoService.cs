using UoW.SERVICE.Dtos;

namespace UoW.SERVICE.Builders.Interfaces
{
    public interface IBaseCargoService
    {
        public string CreateCargo(CreateCargoDto createCargoDto);
        public CargoTrackingInformation GetCargoTrackingInformation(string cargoKey);
    }
}
