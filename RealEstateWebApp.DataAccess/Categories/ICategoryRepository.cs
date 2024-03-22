using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.Category;

namespace RealEstateWebApp.DataAccess.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<bool> Create(CategoryEditModel category);
        Task<bool> Update(CategoryEditModel category);
        Task<bool> Remove(int id);
        Task<IEnumerable<CategoryWithRecordsCountModel>> GetAllWithRecordsCount();
        Task<IEnumerable<TitleAndIdModel>> GetAllTitleOnly();
        Task<TitleAndIdModel> GetOneTitleOnly(int categoryId);
    }
}