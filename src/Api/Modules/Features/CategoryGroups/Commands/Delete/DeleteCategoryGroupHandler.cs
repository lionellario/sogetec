namespace Api.Modules.Features.CategoryGroups.Commands.Delete;

public sealed class DeleteCategoryGroupHandler(SogetecDbContext db) : ICommandHandler<DeleteCategoryGroupCommand>
{
    public async ValueTask<Unit> Handle(DeleteCategoryGroupCommand command, CancellationToken cancellationToken)
    {

        var group = await db.CategoryGroups.FirstOrDefaultAsync(g => g.Id == command.Id, cancellationToken: cancellationToken);

        if (group is null)
        {
            throw NotFoundException.For<CategoryGroup>(
                command.Id,
                CategoryErrorCode.CategoryGroupNotFound);
        }

        db.Remove(group);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}