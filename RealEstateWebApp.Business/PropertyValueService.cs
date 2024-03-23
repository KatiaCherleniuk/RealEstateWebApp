using RealEstateWebApp.DataAccess.Repositories.Properties;
using RealEstateWebApp.Models.PropertyValue;
using RealEstateWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.Business
{
    public class PropertyValueService
    {
        private readonly IPropertyValueRepository _propertyValueRepository;

        public PropertyValueService(IPropertyValueRepository propertyValueRepository)
        {
            _propertyValueRepository = propertyValueRepository;
        }

        public Task<IEnumerable<TitleAndIdModel>> GetAllTitleOnly(int propertyId)
        {
            return _propertyValueRepository.GetAllTitleOnly(propertyId);
        }

        public Task<bool> Create(TitleAndIdModel value, int propertyId)
        {
            return _propertyValueRepository.Create(new PropertyValueEditModel()
            {
                Title = value.Title,
                PropertyId = propertyId
            });
        }

        public Task Update(TitleAndIdModel value, int propertyId)
        {
            return _propertyValueRepository.Update(new PropertyValueEditModel()
            {
                Id = value.Id,
                Title = value.Title,
                PropertyId = propertyId
            });
        }
    }
}
