using colekta_api.Data;
using colekta_api.Models.Entities;
using colekta_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace colekta_api.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryModel?> GetByIdAsync(Guid id)
    {
        CategoryModel? category = await _context.Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return category;
    }

    public IQueryable<CategoryModel> GetAllAsync()
    {
        return _context.Categories.AsNoTracking().AsQueryable();
    }

    public Task<CategoryModel> CreateCategoryAsync(CategoryModel categoryModel)
    {
        _context.Categories.Add(categoryModel);
        return _context.SaveChangesAsync()
            .ContinueWith(_ => categoryModel);
    }
}