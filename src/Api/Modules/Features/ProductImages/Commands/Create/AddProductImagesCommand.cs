namespace Api.Modules.Features.ProductImages.Commands.Create;

public record AddProductImagesRecord(
    int ProductImageId,
    int ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record AddProductImageDto(
    int ProductImageId,
    int ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record AddProductImagesCommand(
    List<AddProductImageDto> Images
) : ICommand<List<AddProductImagesRecord>>;