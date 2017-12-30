using auto.Database;
using auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auto.Services
{
    public interface ITypeModelFetcher
    {
        IEnumerable<TypeViewModel> GetAllTypeViewModels();
        TypeViewModel GetTypeViewModel(int id);
        int Save(TypeViewModel type);
    }

    public class TypeModelFetcher : ITypeModelFetcher
    {
        private readonly IDataRepository _repository;
        private readonly ICarModelFetcher _carModelFetcher;

        public TypeModelFetcher(IDataRepository repository, ICarModelFetcher carModelFetcher)
        {
            _repository = repository;
            _carModelFetcher = carModelFetcher;
        }
        
        public IEnumerable<TypeViewModel> GetAllTypeViewModels()
        {
            var types = new List<TypeViewModel>();
            foreach (var type in _repository.GetAllTypes())
            {
                var typeCars = new List<CarViewModel>();
                foreach (var car in _carModelFetcher.GetAllCarViewModels())
                {
                    if (car.Type.Equals(type))
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

        public TypeViewModel GetTypeViewModel(int id)
        {
            return ConvertToDto(_repository.GetTypeById(id));
        }

        public int Save(TypeViewModel type)
        {

            return _repository.SaveType(type);
        }

        private TypeViewModel ConvertToDto(CarType x)
        {
            if (x == null)
            {
                return null;
            }
            return new TypeViewModel()
            {
                Brand = x.Brand,
                Model = x.Model,
                Id = x.Id
            };
        }
    }
}
