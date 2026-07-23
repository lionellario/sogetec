namespace Api.Modules.Features.CategoryGroups.Commands.Delete;

public sealed class DeleteCategoryGroupHandler(SogetecDbContext db) : ICommandHandler<DeleteCategoryGroupCommand>
{
    public async ValueTask<Unit> Handle(DeleteCategoryGroupCommand command, CancellationToken cancellationToken)
    {

        var groups = await db.CategoryGroups.Where(g => command.Ids.Contains(g.Id)).ToListAsync(cancellationToken);

        if (groups.Count == 0)
        {
            throw NotFoundException.For<CategoryGroup>(
                "category groups",
                CategoryErrorCode.CategoryGroupNotFound);
        }

        db.RemoveRange(groups);

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}