namespace Api.Modules.Features.ProductAttributes.Commands.Create;

public sealed class CreateProductAttributeHandler(SogetecDbContext db)
    : ICommandHandler<CreateProductAttributeCommand, CreateProductAttributeResponse>
{
    public async ValueTask<CreateProductAttributeResponse> Handle(
        CreateProductAttributeCommand command,
        CancellationToken cancellationToken)
    {
        var header = await db.ProductAttributeHeaders.FirstOrDefaultAsync(c => c.Id == command.HeaderId, cancellationToken);

        if (header is null)
        {
            throw NotFoundException.For<ProductAttributeHeader>(
                command.HeaderId,
                ProductErrorCode.ProductAttributeHeaderNotFound);
        }

        var attribute = ProductAttribute.Create(
            header: header,
            name: command.Name,
            nameFr: command.NameFr,
            isVariant: command.IsVariant
        );

        db.Add(attribute);

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: attribute.Id,
            Name: attribute.Name,
            NameFr: attribute.NameFr,
            HeaderId: attribute.HeaderId,
            IsVariant: attribute.IsVariant,
            CreatedAt: attribute.CreatedOn,
            LastModifiedAt: attribute.LastModifiedOn
        );
    }
}