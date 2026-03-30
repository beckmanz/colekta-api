using colekta_api.Models.Enums;

namespace colekta_api.Models.Entities;

public class OrderModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public string PaymentReference { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public DateTime? PaymentDate { get; set; }

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUserModel User { get; set; } = null!;

    public Guid AddressId { get; set; }
    public virtual AddressModel ShippingAddress { get; set; } = null!;

    public virtual ICollection<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
}

