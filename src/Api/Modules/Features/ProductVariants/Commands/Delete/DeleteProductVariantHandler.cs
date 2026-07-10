namespace Api.Modules.Features.ProductVariants.Commands.Delete;

public sealed class DeleteProductVariantHandler(SogetecDbContext db) : ICommandHandler<DeleteProductVariantCommand>
{
    public async ValueTask<Unit> Handle(DeleteProductVariantCommand command, CancellationToken cancellationToken)
    {
        var variant = await db.ProductVariants.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (variant is null)
        {
            throw NotFoundException.For<ProductVariant>(
                command.Id,
                ProductErrorCode.ProductVariantNotFound);
        }

        db.Remove(variant);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}