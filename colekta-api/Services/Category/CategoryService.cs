using colekta_api.Models.ResponseDtos;
using colekta_api.Models.ResultsModel;
using colekta_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace colekta_api.Services.Category;

public class CategoryService : ICategoryInterface
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IResult> GetAllCategories(int  Page, int PageSize)
    {
        var query = _categoryRepository.GetAllAsync();
        
        var totalItems = await query.CountAsync();
        var itemsToSkip = (Page - 1) * PageSize;
        
        var categories = await query.Skip(itemsToSkip)
            .Take(PageSize)
            .ToListAsync();
        
        var itemsDto = categories
            .Select(c => CategoryResponseDto.ToDto(c)).ToList();
        
        var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
        
        var response = new PagedResponseDto<CategoryResponseDto>(
            Items: itemsDto,
            TotalItems: totalItems,
            CurrentPage: Page,
            TotalPages: totalPages
        );
        
        return response.ToOkResult("Categorias listadas com sucesso");
    }
}