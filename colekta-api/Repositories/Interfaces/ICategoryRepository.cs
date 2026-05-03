using colekta_api.Models.Entities;
using colekta_api.Models.FiltersDto;

namespace colekta_api.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<CategoryModel?> GetByIdAsync(Guid id);
    IQueryable<CategoryModel> GetAllAsync(CategoryFilterDto filter);
    Task<CategoryModel> CreateCategoryAsync(CategoryModel categoryModel);
}