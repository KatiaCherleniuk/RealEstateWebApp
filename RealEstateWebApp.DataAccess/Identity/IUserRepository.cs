using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models.User;

namespace RealEstateWebApp.DataAccess.Repositories.Identity
{
    public interface IUserRepository
    {
        Task<bool> Create(ApplicationUser user);
        Task<bool> Update(ApplicationUser user);
        Task<bool> Remove(int id);
        Task<ApplicationUser> GetById(int id);
        Task<ApplicationUser> GetByNormalizedName(string normalizedUserName);
        Task<ApplicationUser> GetByNormalizedEmail(string normalizedEmail);
        Task AddToRole(int userId, int roleId);
        Task RemoveFromRole(int userId, int roleId);
        Task<IEnumerable<ApplicationUser>> GetByRoleId(int roleId);
        Task<IEnumerable<UserCreateModel>> GetAllUsersForEdit();
        Task<bool> SetBlockForUser(int userId, bool isBlocked);
    }
}