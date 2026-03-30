namespace colekta_api.Models.Entities;

public class ProductModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Condition { get; set; } = string.Empty;

    public virtual ICollection<ProductImageModel> Images { get; set; } = new List<ProductImageModel>();

    public Guid CategoryId { get; set; }
    public virtual CategoryModel Category { get; set; } = null!;

    public string SellerId { get; set; } = string.Empty;
    public virtual ApplicationUserModel Seller { get; set; } = null!;
}

