namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Create;

public sealed class CreateProductAttributeHeaderHandler(SogetecDbContext db)
    : ICommandHandler<CreateProductAttributeHeaderCommand, CreateProductAttributeHeaderResponse>
{
    public async ValueTask<CreateProductAttributeHeaderResponse> Handle(
        CreateProductAttributeHeaderCommand command,
        CancellationToken cancellationToken)
    {
        var count = await db.ProductAttributeHeaders.CountAsync(cancellationToken);

        var header = ProductAttributeHeader.Create(
            name: command.Name,
            nameFr: command.NameFr,
            order: count + 1
        );

        db.Add(header);

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: header.Id,
            Name: header.Name,
            NameFr: header.NameFr,
            SortOrder: header.SortOrder,
            CreatedAt: header.CreatedOn,
            LastModifiedAt: header.LastModifiedOn
        );
    }
}