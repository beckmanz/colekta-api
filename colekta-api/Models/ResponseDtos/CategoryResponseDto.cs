using colekta_api.Models.Entities;

namespace colekta_api.Models.ResponseDtos;

public class CategoryResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    
    public static CategoryResponseDto ToDto(CategoryModel category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug
        };
    }
}