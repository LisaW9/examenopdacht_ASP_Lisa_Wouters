using auto.Database;
using auto.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Services
{
    public interface IModelFetcher
    {
        IEnumerable<CarViewModel> GetAllCarViewModels();
        CarViewModel GetCarViewModel(int id);
        int Save(CarViewModel car);
        int Delete(CarViewModel car);

        IEnumerable<OwnerViewModel> GetAllOwnerViewModels();
        IEnumerable<TypeViewModel> GetAllTypeViewModels();
    }

    public class ModelFetcher : IModelFetcher
    {
        private readonly IDataRepository _repository;

        public ModelFetcher(IDataRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CarViewModel> GetAllCarViewModels()
        {
            var owners = _repository.GetAllOwners().Select(ConvertOwnersToSelectListItem).ToList();
            var types = _repository.GetAllTypes().Select(ConvertTypesToSelectListItem).ToList();
            return _repository.GetAllCars().Select(x => ConvertToDto(x, owners, types)).ToList();
        }

        public CarViewModel GetCarViewModel(int id)
        {
            var owners = _repository.GetAllOwners().Select(ConvertOwnersToSelectListItem).ToList();
            var types = _repository.GetAllTypes().Select(ConvertTypesToSelectListItem).ToList();
            return ConvertToDto(_repository.GetCarById(id), owners, types);
        }

        private SelectListItem ConvertOwnersToSelectListItem(Owner owner)
        {
            if (owner == null)
            {
                return null;
            }
            return new SelectListItem()
            {
                Text = owner.FirstName + " " + owner.Name,
                Value = $"{owner.Id}"
            };
        }

        private SelectListItem ConvertTypesToSelectListItem(CarType type)
        {
            if (type == null)
            {
                return null;
            }
            return new SelectListItem()
            {
                Text = type.Brand + " " + type.Model,
                Value = $"{type.Id}"
            };
        }

        public int Save(CarViewModel car)
        {

            return _repository.Save(car);
        }

        public int Delete(CarViewModel car)
        {

            return _repository.Delete(car);
        }

        private CarViewModel ConvertToDto(Car x, List<SelectListItem> owners, List<SelectListItem> types)
        {
            if (x == null)
            {
                return null;
            }
            return new CarViewModel()
            {
                Id = x.Id,
                Color = x.Color,
                PurchaseDate = x.PurchaseDate,
                LicensePlate = x.LicensePlate,
                Owners = owners,
                OwnerId = x.Owner?.Id,
                OwnerLabel = x.Owner?.FirstName + " " + x.Owner?.Name,
                Types = types,
                TypeId = x.Type?.Id,
                TypeLabel = x.Type?.Brand + " " + x.Type?.Model,
            };
        }

        public IEnumerable<OwnerViewModel> GetAllOwnerViewModels()
        {
            var owners = new List<OwnerViewModel>();
            foreach (var owner in _repository.GetAllOwners())
            {
                var ownerCars = new List<CarViewModel>();
                foreach (var car in GetAllCarViewModels())
                {
                    if (car.OwnerLabel.Equals(owner.FirstName + " " + owner.Name))
                    {
                        ownerCars.Add(car);
                    }
                }

                var ownerView = new OwnerViewModel()
                {
                    Id = owner.Id,
                    FirstName = owner.FirstName,
                    Name = owner.Name,
                    Cars = ownerCars
                };
                owners.Add(ownerView);
            }
            return owners;
        }

        public IEnumerable<TypeViewModel> GetAllTypeViewModels()
        {
            var types = new List<TypeViewModel>();
            foreach (var type in _repository.GetAllTypes())
            {
                var typeCars = new List<CarViewModel>();
                foreach (var car in GetAllCarViewModels())
                {
                    if (car.TypeLabel.Equals(type.Brand + " " + type.Model))
                    {
                        typeCars.Add(car);
                    }
                }

                var typeView = new TypeViewModel()
                {
                    Id = type.Id,
                    Brand = type.Brand,
                    Model = type.Model,
                    Cars = typeCars
                };
                types.Add(typeView);
            }
            return types;
        }
    }
}
