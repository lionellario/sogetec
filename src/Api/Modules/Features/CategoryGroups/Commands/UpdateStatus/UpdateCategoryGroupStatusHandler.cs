namespace Api.Modules.Features.CategoryGroups.Commands.UpdateStatus;

public sealed class UpdateCategoryGroupStatusHandler(SogetecDbContext db) : ICommandHandler<UpdateCategoryGroupStatusCommand>
{
    public async ValueTask<Unit> Handle(UpdateCategoryGroupStatusCommand command, CancellationToken cancellationToken)
    {

        var groups = await db.CategoryGroups.Where(g => command.Ids.Contains(g.Id)).ToListAsync(cancellationToken);

        if (groups.Count == 0)
        {
            throw NotFoundException.For<CategoryGroup>(
                string.Join(',', command.Ids),
                CategoryErrorCode.CategoryGroupNotFound);
        }

        foreach (var group in groups)
        {
            group.IsActive = command.IsActive;
        }

        await db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}