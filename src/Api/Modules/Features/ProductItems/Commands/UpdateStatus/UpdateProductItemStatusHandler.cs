namespace Api.Modules.Features.ProductItems.Commands.UpdateStatus;

public sealed class UpdateProductStatusHandler(SogetecDbContext db) : ICommandHandler<UpdateProductItemStatusCommand>
{
    public async ValueTask<Unit> Handle(UpdateProductItemStatusCommand command, CancellationToken cancellationToken)
    {

        var items = await db.ProductItems.Where(g => command.Ids.Contains(g.Id)).ToListAsync(cancellationToken);

        if (items.Count == 0)
        {
            throw NotFoundException.For<ProductItem>(
                "product items",
                ProductErrorCode.ProductItemNotFound);
        }

        foreach (var item in items)
        {
            item.IsActive = command.IsActive;
        }

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}