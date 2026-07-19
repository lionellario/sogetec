namespace Api.Modules.Features.ProductImages.Commands.Create;

public sealed class AddProductImagesHandler(SogetecDbContext db) : ICommandHandler<AddProductImagesCommand, List<AddProductImagesRecord>>
{
    public async ValueTask<List<AddProductImagesRecord>> Handle(AddProductImagesCommand command, CancellationToken cancellationToken)
    {
        var productId = command.ProductId;
        var product = await db.Products.FirstOrDefaultAsync(c => c.Id == productId, cancellationToken);

        var products = new List<ProductImage>();

        if (product is null)
        {
            throw NotFoundException.For<Product>(
                productId,
                ProductErrorCode.NotFound);
        }

        foreach (var image in command.Images)
        {
            var productImage = ProductImage.Create(
                url: image.ImageUrl,
                previewUrl: image.ThumbnailImageUrl,
                product: product);

            products.Add(productImage);
            db.Add(productImage);
        }

        await db.SaveChangesAsync(cancellationToken);

        var response = products
                        .Select(x =>
                            new AddProductImagesRecord(
                                x.Id,
                                x.ProductId,
                                x.Url,
                                x.PreviewUrl))
                        .ToList();

        return response;
    }
}