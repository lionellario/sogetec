namespace Api.Modules.Features.ProductAttributes.Commands.Update;

public sealed class UpdateProductAttributeHandler(SogetecDbContext db)
    : ICommandHandler<UpdateProductAttributeCommand, UpdateProductAttributeResponse>
{
    public async ValueTask<UpdateProductAttributeResponse> Handle(
        UpdateProductAttributeCommand command,
        CancellationToken cancellationToken)
    {
        var models = await db.ProductAttributeHeaders.ToListAsync(cancellationToken);
        var modelItems = await db.ProductAttributes.ToListAsync(cancellationToken);

        var desiredHeader = models.FirstOrDefault(g => g.Id == command.HeaderId);

        if (desiredHeader is null)
        {
            throw NotFoundException.For<ProductAttributeHeader>(
                command.HeaderId,
                ProductErrorCode.ProductAttributeHeaderNotFound);
        }
        var modelItem = modelItems.FirstOrDefault(g => g.Id == command.Id);

        if (modelItem is null)
        {
            throw NotFoundException.For<ProductAttribute>(
                command.Id,
                ProductErrorCode.ProductAttributeNotFound);
        }

        modelItem.Name = command.Name;
        modelItem.NameFr = command.NameFr;
        modelItem.HeaderId = desiredHeader.Id;
        modelItem.Header = desiredHeader;
        modelItem.IsVariant = command.IsVariant;

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: modelItem.Id,
            Name: modelItem.Name,
            NameFr: modelItem.NameFr,
            HeaderId: modelItem.HeaderId,
            IsVariant: modelItem.IsVariant,
            CreatedAt: modelItem.CreatedOn,
            LastModifiedAt: modelItem.LastModifiedOn
        );
    }
}