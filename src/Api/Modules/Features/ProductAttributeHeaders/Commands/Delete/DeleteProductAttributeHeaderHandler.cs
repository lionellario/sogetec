namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Delete;

public sealed class DeleteProductAttributeHeaderHandler(SogetecDbContext db) : ICommandHandler<DeleteProductAttributeHeaderCommand>
{
    public async ValueTask<Unit> Handle(DeleteProductAttributeHeaderCommand command, CancellationToken cancellationToken)
    {
        var header = await db.ProductAttributeHeaders.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (header is null)
        {
            throw NotFoundException.For<ProductAttributeHeader>(
                command.Id,
                ProductErrorCode.ProductAttributeHeaderNotFound);
        }

        db.Remove(header);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}