using colekta_api.Models.Entities;

namespace colekta_api.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<CategoryModel?> GetByIdAsync(Guid id);
}