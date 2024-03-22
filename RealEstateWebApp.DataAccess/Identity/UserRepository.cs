using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models.User;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess.Repositories.Identity
{
    public class UserRepository : DataController, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration, "Users")
        {
        }

        public Task<bool> Create(ApplicationUser user)
        {
            return InsertWithIdAsync(user);
        }

        public Task<bool> Update(ApplicationUser user)
        {
            return UpdateAsync(user);
        }

        public Task<bool> Remove(int id)
        {
            return DeleteByIdAsync(id);
        }

        public Task<ApplicationUser> GetById(int id)
        {
            return GetByIdAsync<int, ApplicationUser>(id);
        }

        public Task<ApplicationUser> GetByNormalizedName(string normalizedUserName)
        {
            return GetOneAsync<ApplicationUser>("GetByNormalizedName", new { NormalizedName = normalizedUserName });
        }

        public Task<ApplicationUser> GetByNormalizedEmail(string normalizedEmail)
        {
            return GetOneAsync<ApplicationUser>("GetByNormalizedEmail", new { NormalizedEmail = normalizedEmail });
        }

        public Task AddToRole(int userId, int roleId)
        {
            return PerformNonQuery("AddToRole", new { UserId = userId, RoleId = roleId });
        }

        public Task RemoveFromRole(int userId, int roleId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetByRoleId(int roleId)
        {
            return GetManyAsync<ApplicationUser>("GetByRoleId", new { RoleId = roleId });
        }

        public Task<IEnumerable<UserCreateModel>> GetAllUsersForEdit()
        {
            return GetManyAsync<UserCreateModel>("GetAllUsersForEdit");
        }

        public Task<bool> SetBlockForUser(int userId, bool isBlocked)
        {
            return PerformNonQuery("SetBlockForUser", new { UserId = userId, IsBlocked = isBlocked });
        }
    }
}