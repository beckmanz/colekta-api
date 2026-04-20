using colekta_api.Models.Entities;

namespace colekta_api.Models.ResponseDtos;

public class ProductImageResponseDto
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public int Order { get; set; }
    public bool IsCover { get; set; }

    public static ProductImageResponseDto ToDto(ProductImageModel imageModel)
    {
        return new ProductImageResponseDto()
        {
            Id = imageModel.Id,
            Url = imageModel.Url,
            Order = imageModel.Order,
            IsCover = imageModel.IsCover
        };
    }
}