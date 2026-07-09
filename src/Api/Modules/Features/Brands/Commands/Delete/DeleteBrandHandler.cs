namespace Api.Modules.Features.Brands.Commands.Delete;

public sealed class DeleteBrandHandler(SogetecDbContext db) : ICommandHandler<DeleteBrandCommand>
{
    public async ValueTask<Unit> Handle(DeleteBrandCommand command, CancellationToken cancellationToken)
    {
        var brand = await db.Brands.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (brand is null)
        {
            throw NotFoundException.For<Brand>(
                command.Id,
                BrandErrorCode.NotFound);
        }

        db.Remove(brand);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}