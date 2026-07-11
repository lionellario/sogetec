namespace Api.Modules.Features.ProductItems.Commands.Delete;

public sealed class DeleteProductItemHandler(SogetecDbContext db) : ICommandHandler<DeleteProductItemCommand>
{
    public async ValueTask<Unit> Handle(DeleteProductItemCommand command, CancellationToken cancellationToken)
    {
        var productItem = await db.ProductItems.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (productItem is null)
        {
            throw NotFoundException.For<ProductItem>(
                command.Id,
                ProductErrorCode.ProductItemNotFound);
        }

        db.Remove(productItem);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}