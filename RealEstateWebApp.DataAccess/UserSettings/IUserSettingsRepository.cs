using System.Threading.Tasks;
using RealEstateWebApp.Models.UserSettings;

namespace RealEstateWebApp.DataAccess.Repositories.UserSettings
{
    public interface IUserSettingsRepository
    {
        Task<UserSettingsEditModel> GetByKeyAndUser(int userId, string key);
        Task<bool> InsertOrUpdate(UserSettingsEditModel userSettingsEditModel);
    }
}