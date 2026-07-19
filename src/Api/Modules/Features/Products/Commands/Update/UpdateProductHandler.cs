using Sogetec.Chassis.Extensions;

namespace Api.Modules.Features.Products.Commands.Update;

public sealed class UpdateProductHandler(SogetecDbContext db) : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async ValueTask<UpdateProductResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var brand = await db.Brands.FirstOrDefaultAsync(x => x.Id == command.BrandId, cancellationToken);
        if (brand is null)
        {
            throw NotFoundException.For<Brand>(
                command.BrandId,
                ProductErrorCode.BrandNotFound);
        }

        var category = await db.Categories.FirstOrDefaultAsync(x => x.Id == command.CategoryId, cancellationToken);
        if (category is null)
        {
            throw NotFoundException.For<Category>(
                command.CategoryId,
                ProductErrorCode.CategoryNotFound);
        }

        var product = await db.Products.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
        if (product is null)
        {
            throw NotFoundException.For<Product>(
                command.Id,
                ProductErrorCode.NotFound);
        }

        product.Name = command.Name;
        product.NameFr = command.NameFr;
        product.Slug = product.Name.Slugify();
        product.Description = command.Description;
        product.IsActive = command.IsActive;
        product.BrandId = brand.Id;
        product.CategoryId = category.Id;
        product.Price = command.Price;
        product.Cost = command.Cost;
        product.QuantityUnit = command.QuantityUnit;

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: product.Id
        );
    }
}