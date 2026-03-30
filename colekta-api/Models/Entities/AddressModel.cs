namespace colekta_api.Models.Entities;

public class AddressModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public bool IsDefault { get; set; }

    public string UserId { get; set; } = string.Empty;
    public virtual ApplicationUserModel User { get; set; } = null!;
}

