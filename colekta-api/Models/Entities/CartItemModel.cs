namespace colekta_api.Models.Entities;

public class CartItemModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; }

    public Guid CartId { get; set; }
    public virtual CartModel Cart { get; set; } = null!;

    public Guid ProductId { get; set; }
    public virtual ProductModel Product { get; set; } = null!;
}

