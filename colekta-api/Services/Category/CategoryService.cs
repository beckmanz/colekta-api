using colekta_api.Helpers;
using colekta_api.Models.Entities;
using colekta_api.Models.FiltersDto;
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

    public async Task<IResult> GetAllCategories(CategoryFilterDto filter)
    {
        var query = _categoryRepository.GetAllAsync(filter);
        
        var totalItems = await query.CountAsync();
        var itemsToSkip = (filter.Page - 1) * filter.PageSize;
        
        var categories = await query.Skip(itemsToSkip)
            .Take(filter.PageSize)
            .ToListAsync();
        
        var itemsDto = categories
            .Select(c => CategoryResponseDto.ToDto(c)).ToList();
        
        var totalPages = (int)Math.Ceiling((double)totalItems / filter.PageSize);
        
        var response = new PagedResponseDto<CategoryResponseDto>(
            Items: itemsDto,
            TotalItems: totalItems,
            CurrentPage: filter.Page,
            TotalPages: totalPages
        );
        
        return response.ToOkResult("Categorias listadas com sucesso");
    }

    public async Task<IResult> CreateCategoryAsync(string nome)
    {
        var nomeExist = await _categoryRepository.GetByNameAsync(nome);
        if (nomeExist is not null)
        {
            return "Já existe uma categoria com esse nome!".ToBadRequestResult();
        }
        
        var category = new CategoryModel
        {
            Name = nome,
            Slug = ValidationUtils.ToSlug(nome)
        };
        await _categoryRepository.CreateCategoryAsync(category);
        var response = CategoryResponseDto.ToDto(category);
        
        return response.ToOkResult("Categoria criada com sucesso");
    }
}