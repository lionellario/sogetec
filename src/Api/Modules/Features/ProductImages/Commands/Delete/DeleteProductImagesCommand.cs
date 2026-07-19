namespace Api.Modules.Features.ProductImages.Commands.Delete;

public record DeleteProductImageDto(
    Guid ProductImageId,
    Guid ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record DeleteProductImagesCommand(
    List<DeleteProductImageDto> Images
) : ICommand;