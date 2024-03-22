using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models.Property;

namespace RealEstateWebApp.DataAccess.Repositories.Properties
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<PropertyWithValuesCountModel>> GetAllWithValuesCount(int categoryId);
        Task<bool> Delete(int id);
        Task<bool> Create(PropertyEditModel property);
        Task<bool> Update(PropertyEditModel property);
        Task<IEnumerable<PropertyWithValuesFlatModel>> GetWithValuesByCategory(int categoryId);
    }
}