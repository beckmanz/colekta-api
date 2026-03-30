namespace colekta_api.Models.Entities;

public class ProductImageModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Url { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool IsCover { get; set; }

    public Guid ProductId { get; set; }
    public virtual ProductModel Product { get; set; } = null!;
}


