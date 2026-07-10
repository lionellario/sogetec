namespace Api.Modules.Features.Categories.Commands.Delete;

public sealed class DeleteCategoryHandler(SogetecDbContext db) : ICommandHandler<DeleteCategoryCommand>
{
    public async ValueTask<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);

        if (category is null)
        {
            throw NotFoundException.For<Category>(
                command.Id,
                CategoryErrorCode.CategoryNotFound);
        }

        db.Remove(category);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}