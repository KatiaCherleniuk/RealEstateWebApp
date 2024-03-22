using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models.Property;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess.Repositories.Properties
{
    public class PropertyRepository : DataController, IPropertyRepository
    {
        public PropertyRepository(IConfiguration configuration) : base(configuration, "Properties")
        {
        }
        public Task<IEnumerable<PropertyWithValuesCountModel>> GetAllWithValuesCount(int categoryId)
        {
            return GetManyAsync<PropertyWithValuesCountModel>("GetAllWithValuesCount", new {CategoryId = categoryId});
        }

        public Task<bool> Delete(int id)
        {
            return DeleteByIdAsync(id);
        }

        public Task<bool> Create(PropertyEditModel property)
        {
            return InsertWithIdAsync(property);
        }

        public Task<bool> Update(PropertyEditModel property)
        {
            return UpdateAsync(property);
        }

        public Task<IEnumerable<PropertyWithValuesFlatModel>> GetWithValuesByCategory(int categoryId)
        {
            return GetManyAsync<PropertyWithValuesFlatModel>("GetWithValuesByCategory", new { CategoryId = categoryId });
        }
    }
}