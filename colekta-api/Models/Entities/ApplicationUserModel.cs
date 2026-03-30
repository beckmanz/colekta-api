using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace colekta_api.Models.Entities;

public class ApplicationUserModel : IdentityUser
{
    public string FullName { get; set; }
    public string? Cpf { get; set; }

    public virtual ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();
    public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
    public virtual CartModel? ShoppingCart { get; set; }
    public virtual ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();
}

