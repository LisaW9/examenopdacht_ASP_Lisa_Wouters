using auto.Database;
using auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Services
{
    public interface ICarModelFetcher
    {
        IEnumerable<CarViewModel> GetAllCarViewModels();
        CarViewModel GetCarViewModel(int id);
        int Save(CarViewModel car);
    }

    public class CarModelFetcher : ICarModelFetcher
    {
        private readonly IDataRepository _repository;

        public CarModelFetcher(IDataRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CarViewModel> GetAllCarViewModels()
        {
            return _repository.GetAllCars().Select(ConvertToDto).ToList();
        }

        public CarViewModel GetCarViewModel(int id)
        {
            return ConvertToDto(_repository.GetCarById(id));
        }

        public int Save(CarViewModel car)
        {

            return _repository.SaveCar(car);
        }

        private CarViewModel ConvertToDto(Car x)
        {
            if (x == null)
            {
                return null;
            }
            return new CarViewModel()
            {
                Color = x.Color,
                PurchaseDate = x.PurchaseDate,
                LicensePlate = x.LicensePlate,
                Owner = x.Owner,
                Type = x.Type,
                Id = x.Id
            };
        }
    }
}
