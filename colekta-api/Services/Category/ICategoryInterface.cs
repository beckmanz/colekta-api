using colekta_api.Models.FiltersDto;

namespace colekta_api.Services.Category;

public interface ICategoryInterface
{
    Task<IResult> GetAllCategories(CategoryFilterDto filter);
}