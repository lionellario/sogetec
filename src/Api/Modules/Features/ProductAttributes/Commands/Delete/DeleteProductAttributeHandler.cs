namespace Api.Modules.Features.ProductAttributes.Commands.Delete;

public sealed class DeleteProductAttributeHandler(SogetecDbContext db) : ICommandHandler<DeleteProductAttributeCommand>
{
    public async ValueTask<Unit> Handle(DeleteProductAttributeCommand command, CancellationToken cancellationToken)
    {
        var attribute = await db.ProductAttributes.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (attribute is null)
        {
            throw NotFoundException.For<ProductAttribute>(
                command.Id,
                ProductErrorCode.ProductAttributeNotFound);
        }

        db.Remove(attribute);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}