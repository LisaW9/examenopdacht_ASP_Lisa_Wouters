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
        int SaveCar(CarViewModel car);

        IEnumerable<Owner> GetAllOwners();
        Owner GetOwnerById(int id);
        int SaveOwner(OwnerViewModel car);

        IEnumerable<CarType> GetAllTypes();
        CarType GetTypeById(int id);
        int SaveType(TypeViewModel car);
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

        public int SaveCar(CarViewModel car)
        {
            var existing = _dataContext.Cars.FirstOrDefault(x => x.Id == car.Id);
            var id = 0;
            if (existing != null)
            {
                existing.Color = car.Color;
                existing.PurchaseDate = car.PurchaseDate;
                existing.LicensePlate = car.LicensePlate;
                existing.Owner = car.Owner;
                existing.Type = car.Type;

                _dataContext.Update(existing);
                id = existing.Id;
            }
            else
            {
                var newCar = new Car();
                newCar.Color = car.Color;
                newCar.PurchaseDate = car.PurchaseDate;
                newCar.LicensePlate = car.LicensePlate;
                newCar.Owner = car.Owner;
                newCar.Type = car.Type;

                _dataContext.Add(newCar);
                id = newCar.Id;
            }
            _dataContext.SaveChanges();
            return id;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _dataContext.Owners.ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return _dataContext.Owners.Find(id);
        }

        public int SaveOwner(OwnerViewModel owner)
        {
            var existing = _dataContext.Owners.FirstOrDefault(x => x.Id == owner.Id);
            var id = 0;
            if (existing != null)
            {
                existing.Name = owner.Name;
                existing.FirstName = owner.FirstName;

                _dataContext.Update(existing);
                id = existing.Id;
            }
            else
            {
                var newOwner = new Owner();
                newOwner.Name = owner.Name;
                newOwner.FirstName = owner.FirstName;

                _dataContext.Add(newOwner);
                id = newOwner.Id;
            }
            _dataContext.SaveChanges();
            return id;
        }

        public IEnumerable<CarType> GetAllTypes()
        {
            return _dataContext.Types.ToList();
        }

        public CarType GetTypeById(int id)
        {
            return _dataContext.Types.Find(id);
        }

        public int SaveType(TypeViewModel type)
        {
            var existing = _dataContext.Types.FirstOrDefault(x => x.Id == type.Id);
            var id = 0;
            if (existing != null)
            {
                existing.Brand = type.Brand;
                existing.Model = type.Model;

                _dataContext.Update(existing);
                id = existing.Id;
            }
            else
            {
                var newType = new CarType();
                newType.Brand = type.Brand;
                newType.Model = type.Model;

                _dataContext.Add(newType);
                id = newType.Id;
            }
            _dataContext.SaveChanges();
            return id;
        }
    }
}
