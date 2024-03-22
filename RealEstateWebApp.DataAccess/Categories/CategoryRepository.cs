using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.Category;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess.Repositories.Categories
{
    public class CategoryRepository : DataController, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration, "Categories")
        {
        }

        public Task<bool> Create(CategoryEditModel category)
        {
            return InsertWithIdAsync(category);
        }

        public Task<bool> Update(CategoryEditModel category)
        {
            return UpdateAsync(category);
        }
        
        public Task<bool> Remove(int id)
        {
            return DeleteByIdAsync(id);
        }

        public Task<IEnumerable<CategoryWithRecordsCountModel>> GetAllWithRecordsCount()
        {
            return GetManyAsync<CategoryWithRecordsCountModel>("GetAllWithRecordsCount");
        }

        public Task<IEnumerable<TitleAndIdModel>> GetAllTitleOnly()
        {
            return GetManyAsync<TitleAndIdModel>("GetAllTitleOnly");
        }

        public Task<TitleAndIdModel> GetOneTitleOnly(int categoryId)
        {
            return GetOneAsync<TitleAndIdModel>("GetOneTitleOnly", new {Id = categoryId});
        }
    }
}