namespace Api.Modules.Features.Products.Commands.UpdateStatus;

public sealed class UpdateProductStatusHandler(SogetecDbContext db) : ICommandHandler<UpdateProductStatusCommand>
{
    public async ValueTask<Unit> Handle(UpdateProductStatusCommand command, CancellationToken cancellationToken)
    {

        var products = await db.Products.Where(g => command.Ids.Contains(g.Id)).ToListAsync(cancellationToken);

        if (products.Count == 0)
        {
            throw NotFoundException.For<Product>(
                "products",
                ProductErrorCode.NotFound);
        }

        foreach (var prod in products)
        {
            prod.IsActive = command.IsActive;
        }

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}