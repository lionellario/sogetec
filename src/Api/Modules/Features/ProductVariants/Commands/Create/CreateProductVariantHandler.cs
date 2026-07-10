namespace Api.Modules.Features.ProductVariants.Commands.Create;

public sealed class CreateProductVariantHandler(SogetecDbContext db)
    : ICommandHandler<CreateProductVariantCommand, CreateProductVariantResponse>
{
    public async ValueTask<CreateProductVariantResponse> Handle(
        CreateProductVariantCommand command,
        CancellationToken cancellationToken)
    {
        var item = await db.ProductItems.FirstOrDefaultAsync(c => c.Id == command.ProductItemId, cancellationToken);
        var attribute = await db.ProductAttributes.FirstOrDefaultAsync(c => c.Id == command.ProductAttributeId, cancellationToken);

        if (item is null)
        {
            throw NotFoundException.For<ProductItem>(
                command.ProductItemId,
                ProductErrorCode.ProductItemNotFound);
        }

        if (attribute is null)
        {
            throw NotFoundException.For<ProductAttribute>(
                command.ProductAttributeId,
                ProductErrorCode.ProductAttributeNotFound);
        }

        var variant = ProductVariant.Create(
            item: item,
            attribute: attribute,
            value: command.Value
        );

        db.Add(variant);

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: variant.Id,
            ProductItemId: variant.ItemId,
            ProductAttributeId: variant.AttributeId,
            ProductAttributeName: attribute.Name,
            ProductAttributeNameFr: attribute.NameFr,
            Value: variant.Value,
            CreatedAt: variant.CreatedOn,
            LastModifiedAt: variant.LastModifiedOn
        );
    }
}