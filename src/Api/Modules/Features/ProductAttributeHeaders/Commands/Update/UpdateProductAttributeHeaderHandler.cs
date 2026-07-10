using Sogetec.Chassis.Extensions;

namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Update;

public sealed class UpdateProductAttributeHeaderHandler(SogetecDbContext db)
    : ICommandHandler<UpdateProductAttributeHeaderCommand, UpdateProductAttributeHeaderResponse>
{
    public async ValueTask<UpdateProductAttributeHeaderResponse> Handle(
        UpdateProductAttributeHeaderCommand command,
        CancellationToken cancellationToken)
    {
        var headers = await db.ProductAttributeHeaders.ToListAsync(cancellationToken);
        var header = headers.FirstOrDefault(g => g.Id == command.Id);

        if (header is null)
        {
            throw NotFoundException.For<ProductAttributeHeader>(
                command.Id,
                ProductErrorCode.ProductAttributeHeaderNotFound);
        }

        header.Name = command.Name;
        header.NameFr = command.NameFr;

        SortOrderChain.PlaceInChain(header, headers, command.SortOrder);

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