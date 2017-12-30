using auto.Database;
using auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Services
{
    public interface IOwnerModelFetcher
    {
        IEnumerable<OwnerViewModel> GetAllOwnerViewModels();
        OwnerViewModel GetOwnerViewModel(int id);
        int Save(OwnerViewModel owner);
    }

    public class OwnerModelFetcher : IOwnerModelFetcher
    {
        private readonly IDataRepository _repository;
        private readonly ICarModelFetcher _carModelFetcher;

        public OwnerModelFetcher(IDataRepository repository, ICarModelFetcher carModelFetcher)
        {
            _repository = repository;
            _carModelFetcher = carModelFetcher;
        }

        public IEnumerable<OwnerViewModel> GetAllOwnerViewModels()
        {
            var owners = new List<OwnerViewModel>();
            foreach(var owner in _repository.GetAllOwners())
            {
                var ownerCars = new List<CarViewModel>();
                foreach(var car in _carModelFetcher.GetAllCarViewModels())
                {
                    if (car.Owner.Equals(owner))
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

        public OwnerViewModel GetOwnerViewModel(int id)
        {
            return ConvertToDto(_repository.GetOwnerById(id));
        }

        public int Save(OwnerViewModel owner)
        {

            return _repository.SaveOwner(owner);
        }

        private OwnerViewModel ConvertToDto(Owner x)
        {
            if (x == null)
            {
                return null;
            }
            return new OwnerViewModel()
            {
                Name = x.Name,
                FirstName = x.FirstName,
                Id = x.Id
            };
        }
    }
}
