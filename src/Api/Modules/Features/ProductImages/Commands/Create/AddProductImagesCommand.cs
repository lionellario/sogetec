namespace Api.Modules.Features.ProductImages.Commands.Create;

public record AddProductImagesRecord(
    Guid ProductImageId,
    Guid ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record AddProductImageDto(
    Guid ProductImageId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record AddProductImagesCommand(
    Guid ProductId,
    List<AddProductImageDto> Images
) : ICommand<List<AddProductImagesRecord>>;