namespace Api.Modules.Features.Products.Commands.Delete;

public sealed class DeleteProductHandler(SogetecDbContext db) : ICommandHandler<DeleteProductCommand>
{
    public async ValueTask<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {

        var product = await db.Products.FirstOrDefaultAsync(g => g.Id == command.Id, cancellationToken: cancellationToken);

        if (product is null)
        {
            throw NotFoundException.For<Product>(
                command.Id,
                ProductErrorCode.NotFound);
        }

        db.Remove(product);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}