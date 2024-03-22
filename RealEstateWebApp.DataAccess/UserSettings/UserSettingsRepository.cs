using System.Threading.Tasks;
using RealEstateWebApp.Models.UserSettings;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess.Repositories.UserSettings
{
    public class UserSettingsRepository: DataController, IUserSettingsRepository
    {
        public UserSettingsRepository(IConfiguration configuration) : base(configuration, "UserSettings")
        {
        }

        public Task<UserSettingsEditModel> GetByKeyAndUser(int userId, string key)
        {
            return GetOneAsync<UserSettingsEditModel>("GetByKeyAndUser", new { UserId = userId, Key = key });
        }

        public Task<bool> InsertOrUpdate(UserSettingsEditModel model)
        {
            return PerformNonQuery("InsertOrUpdate", model);
        }
    }
}