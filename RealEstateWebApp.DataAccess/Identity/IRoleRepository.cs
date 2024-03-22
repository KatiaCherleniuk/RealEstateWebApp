using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.User;

namespace RealEstateWebApp.DataAccess.Repositories.Identity
{
    public interface IRoleRepository
    {
        Task<bool> Create(ApplicationRole role);
        Task<bool> Update(ApplicationRole role);
        Task<bool> Remove(int id);
        Task<ApplicationRole> GetById(int id);
        Task<ApplicationRole> GetByNormalizedName(string normalizedRoleName);
        Task<IEnumerable<TitleAndIdModel>> GetAll();
    }
}