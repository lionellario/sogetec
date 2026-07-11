namespace Api.Modules.Features.ProductVariants.Commands.Update;

public sealed class UpdateProductVariantHandler(SogetecDbContext db)
    : ICommandHandler<UpdateProductVariantCommand, UpdateProductVariantResponse>
{
    public async ValueTask<UpdateProductVariantResponse> Handle(
        UpdateProductVariantCommand command,
        CancellationToken cancellationToken)
    {
        var variant = await db.ProductVariants.Include(c => c.Attribute).FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (variant is null)
        {
            throw NotFoundException.For<ProductVariant>(
                command.Id,
                ProductErrorCode.ProductVariantNotFound);
        }

        variant.Value = command.Value;

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: variant.Id,
            ProductItemId: variant.ItemId,
            ProductAttributeId: variant.AttributeId,
            ProductAttributeName: variant.Attribute!.Name,
            ProductAttributeNameFr: variant.Attribute!.NameFr,
            Value: variant.Value,
            CreatedAt: variant.CreatedOn,
            LastModifiedAt: variant.LastModifiedOn
        );
    }
}