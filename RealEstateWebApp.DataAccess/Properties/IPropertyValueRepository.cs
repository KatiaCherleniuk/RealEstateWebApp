using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.PropertyValue;

namespace RealEstateWebApp.DataAccess.Repositories.Properties
{
    public interface IPropertyValueRepository
    {
        Task<IEnumerable<TitleAndIdModel>> GetAllTitleOnly(int propertyId);
        Task<bool> Create(PropertyValueEditModel model);
        Task<bool> Update(PropertyValueEditModel model);
    }
}