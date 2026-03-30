namespace colekta_api.Models.Entities;

public class CartModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUserModel User { get; set; } = null!;

    public virtual ICollection<CartItemModel> Items { get; set; } = new List<CartItemModel>();
}

