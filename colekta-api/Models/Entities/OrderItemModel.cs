namespace colekta_api.Models.Entities;

public class OrderItemModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Guid OrderId { get; set; }
    public virtual OrderModel Order { get; set; } = null!;

    public Guid ProductId { get; set; }
    public virtual ProductModel Product { get; set; } = null!;
}

