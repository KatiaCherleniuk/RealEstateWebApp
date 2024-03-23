using RealEstateWebApp.DataAccess.Repositories.Properties;
using RealEstateWebApp.Models.Property;
using RealEstateWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.Business
{
    public class PropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly PropertyValueService _propertyValueService;

        public PropertyService(IPropertyRepository propertyRepository, PropertyValueService propertyValueService)
        {
            _propertyRepository = propertyRepository;
            _propertyValueService = propertyValueService;
        }

        public Task<IEnumerable<PropertyWithValuesCountModel>> GetAllWithValuesCount(int categoryId)
        {
            return _propertyRepository.GetAllWithValuesCount(categoryId);
        }

        public Task<bool> Delete(int id)
        {
            return _propertyRepository.Delete(id);
        }

        public async Task Create(PropertyWithValuesCountModel property, IEnumerable<TitleAndIdModel> values)
        {
            await _propertyRepository.Create(property);

            var tasks = new List<Task>();
            foreach (var value in values)
            {
                var t = _propertyValueService.Create(value, property.Id);
                tasks.Add(t);
            }

            await Task.WhenAll(tasks);
        }

        public async Task Update(PropertyWithValuesCountModel property, IEnumerable<TitleAndIdModel> values)
        {
            await _propertyRepository.Update(property);

            var tasks = new List<Task>();
            foreach (var value in values)
            {
                Task t = value.Id == 0
                    ? _propertyValueService.Create(value, property.Id)
                    : _propertyValueService.Update(value, property.Id);
                tasks.Add(t);
            }

            await Task.WhenAll(tasks);
        }

        public Task<IEnumerable<PropertyWithValuesModel>> GetFiltersForCategory(int categoryId)
        {
            return GetAllForCategory(categoryId);
        }

        public async Task<IEnumerable<PropertyWithValuesModel>> GetAllForCategory(int categoryId)
        {
            var flatItems = await _propertyRepository.GetWithValuesByCategory(categoryId);
            var res = flatItems
                .GroupBy(i => i.Id)
                .Select(g =>
                    new PropertyWithValuesModel()
                    {
                        Id = g.Key,
                        Title = g.First().Title,
                        Type = g.First().Type,
                        Values = g.Select(gi =>
                                new TitleAndIdModel()
                                {
                                    Id = gi.ValueId,
                                    Title = gi.ValueTitle
                                })
                            .ToList()
                    })
                .ToList();
            return res;
        }
    }
}
