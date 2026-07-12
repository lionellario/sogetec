namespace Api.Modules.Features.Products.Commands.Create;

public sealed class CreateProductHandler(SogetecDbContext db) : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    public async ValueTask<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var brand = await db.Brands.FirstOrDefaultAsync(x => x.Id == command.BrandId, cancellationToken);
        var category = await db.Categories.FirstOrDefaultAsync(x => x.Id == command.CategoryId, cancellationToken);

        if (brand is null)
        {
            throw NotFoundException.For<Brand>(
                command.BrandId,
                ProductErrorCode.BrandNotFound);
        }

        if (category is null)
        {
            throw NotFoundException.For<Category>(
                command.CategoryId,
                ProductErrorCode.CategoryNotFound);
        }

        var product = Product.Create(
            name: command.Name,
            nameFr: command.NameFr,
            description: command.Description,
            brand: brand,
            category: category,
            isActive: command.IsActive
        );

        db.Add(product);

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: product.Id,
            Name: product.Name,
            NameFr: product.NameFr,
            Slug: product.Slug,
            Description: product.Description,
            IsActive: product.IsActive
        );
    }
}