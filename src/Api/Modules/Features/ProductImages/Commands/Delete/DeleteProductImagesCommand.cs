namespace Api.Modules.Features.ProductImages.Commands.Delete;

public record DeleteProductImageDto(
    int ProductImageId,
    int ProductId,
    string ImageUrl,
    string ThumbnailImageUrl
);

public record DeleteProductImagesCommand(
    List<DeleteProductImageDto> Images
) : ICommand;