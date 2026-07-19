namespace Api.Modules.Features.Brands.Commands.Delete;

public sealed class DeleteBrandHandler(SogetecDbContext db) : ICommandHandler<DeleteBrandCommand>
{
    public async ValueTask<Unit> Handle(DeleteBrandCommand command, CancellationToken cancellationToken)
    {
        var brands = await db.Brands.Where(c => command.Ids.Contains(c.Id)).ToListAsync(cancellationToken);

        if (brands.Count == 0)
        {
            throw NotFoundException.For<Brand>(
                "Brands",
                BrandErrorCode.NotFound);
        }

        db.RemoveRange(brands);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}