using colekta_api.Models.Entities;

namespace colekta_api.Models.ResponseDtos;

public class SellerResponseDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }

    public static SellerResponseDto ToDto(ApplicationUserModel user)
    {
        var result =  new SellerResponseDto()
        {
            Id = Guid.Parse(user.Id),
            FullName = user.FullName
        };
        return result;
    }
}