namespace colekta_api.Models.Entities;

public class CategoryModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

    public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}

