using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.PropertyValue;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess.Repositories.Properties
{
    public class PropertyValueRepository : DataController, IPropertyValueRepository
    {
        public PropertyValueRepository(IConfiguration configuration) : base(configuration, "PropertyValues")
        {
        }
        public Task<IEnumerable<TitleAndIdModel>> GetAllTitleOnly(int propertyId)
        {
            return GetManyAsync<TitleAndIdModel>("GetAllTitleOnly", new {PropertyId = propertyId});
        }

        public Task<bool> Create(PropertyValueEditModel model)
        {
            return InsertWithIdAsync(model);
        }

        public Task<bool> Update(PropertyValueEditModel model)
        {
            return UpdateAsync(model);
        }
    }
}