namespace Api.Modules.Features.ProductImages.Commands.Delete;

public sealed class DeleteProductImagesHandler(SogetecDbContext db) : ICommandHandler<DeleteProductImagesCommand>
{
    public async ValueTask<Unit> Handle(DeleteProductImagesCommand command, CancellationToken cancellationToken)
    {
        var productImgIds = command.Images.Select(x => x.ProductImageId).ToList();
        var productImagesToRemove = await db.ProductImages.Where(c => productImgIds.Contains(c.Id)).ToListAsync(cancellationToken);

        db.RemoveRange(productImagesToRemove);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}