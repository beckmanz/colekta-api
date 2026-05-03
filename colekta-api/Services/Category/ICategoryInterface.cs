namespace colekta_api.Services.Category;

public interface ICategoryInterface
{
    Task<IResult> GetAllCategories(int Page, int PageSize);
}