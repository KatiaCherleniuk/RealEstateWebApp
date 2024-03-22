using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.User;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess.Repositories.Identity
{
    public class RoleRepository : DataController, IRoleRepository
    {
        public RoleRepository(IConfiguration configuration) : base(configuration, "Roles")
        {
        }

        public Task<bool> Create(ApplicationRole role)
        {
            return InsertWithIdAsync(role);
        }

        public Task<bool> Update(ApplicationRole role)
        {
            return UpdateAsync(role);
        }

        public Task<bool> Remove(int id)
        {
            return DeleteByIdAsync(id);
        }

        public Task<ApplicationRole> GetById(int id)
        {
            return GetByIdAsync<int, ApplicationRole>(id);
        }

        public Task<ApplicationRole> GetByNormalizedName(string normalizedRoleName)
        {
            return GetOneAsync<ApplicationRole>("GetByNormalizedName", new { NormalizedRoleName = normalizedRoleName });
        }

        public Task<IEnumerable<TitleAndIdModel>> GetAll()
        {
            return GetManyAsync<TitleAndIdModel>("GetAllTitleOnly");
        }
    }
}