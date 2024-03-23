using RealEstateWebApp.DataAccess.Repositories.Categories;
using RealEstateWebApp.Models.Category;
using RealEstateWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.Business
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<bool> Create(CategoryEditModel category)
        {
            return _categoryRepository.Create(category);
        }

        public Task<bool> Update(CategoryEditModel category)
        {
            return _categoryRepository.Update(category);
        }

        public Task<bool> Remove(int id)
        {
            return _categoryRepository.Remove(id);
        }

        public Task<IEnumerable<CategoryWithRecordsCountModel>> GetAllWithRecordsCount()
        {
            return _categoryRepository.GetAllWithRecordsCount();
        }

        public Task<IEnumerable<TitleAndIdModel>> GetAllTitleOnly()
        {
            return _categoryRepository.GetAllTitleOnly();
        }

        public Task<TitleAndIdModel> GetOneTitleOnly(int categoryId)
        {
            return _categoryRepository.GetOneTitleOnly(categoryId);
        }
    }
}
