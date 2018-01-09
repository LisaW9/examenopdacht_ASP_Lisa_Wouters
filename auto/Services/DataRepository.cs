using auto.Database;
using auto.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Services
{
    public interface IDataRepository
    {
        IEnumerable<Car> GetAllCars();
        Car GetCarById(int id);
        int Save(CarViewModel car);
        int Delete(CarViewModel car);

        IEnumerable<Owner> GetAllOwners();

        IEnumerable<CarType> GetAllTypes();
    }

    public class DataRepository : IDataRepository
    {
        private readonly PersonalDataContext _dataContext;

        public DataRepository(PersonalDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Car> GetAllCars()
        {
            return _dataContext.Cars.Include(x => x.Owner).Include(x => x.Type).ToList();
        }

        public Car GetCarById(int id)
        {
            return _dataContext.Cars.Find(id);
        }

        public int Save(CarViewModel car)
        {
            var existing = _dataContext.Cars.FirstOrDefault(x => x.Id == car.Id);
            var id = 0;
            if (existing != null)
            {
                existing.Color = car.Color;
                existing.PurchaseDate = car.PurchaseDate;
                existing.LicensePlate = car.LicensePlate;
                existing.Owner = _dataContext.Owners.Find(car.OwnerId);
                existing.Type = _dataContext.Types.Find(car.TypeId);

                _dataContext.Update(existing);
                id = existing.Id;
            }
            else
            {
                var newCar = new Car();
                newCar.Color = car.Color;
                newCar.PurchaseDate = car.PurchaseDate;
                newCar.LicensePlate = car.LicensePlate;
                newCar.Owner = _dataContext.Owners.Find(car.OwnerId);
                newCar.Type = _dataContext.Types.Find(car.TypeId);

                _dataContext.Add(newCar);
                id = newCar.Id;
            }
            _dataContext.SaveChanges();
            return id;
        }

        public int Delete(CarViewModel car)
        {
            var existing = _dataContext.Cars.FirstOrDefault(x => x.Id == car.Id);
            _dataContext.Remove(existing);
            _dataContext.SaveChanges();
            return existing.Id;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _dataContext.Owners.ToList();
        }

        public IEnumerable<CarType> GetAllTypes()
        {
            return _dataContext.Types.ToList();
        }
    }
}
