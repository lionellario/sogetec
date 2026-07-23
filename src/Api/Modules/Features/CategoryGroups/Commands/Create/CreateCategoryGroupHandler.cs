namespace Api.Modules.Features.CategoryGroups.Commands.Create;

public sealed class CreateCategoryGroupHandler(SogetecDbContext db) : ICommandHandler<CreateCategoryGroupCommand, CreateCategoryGroupResponse>
{
    public async ValueTask<CreateCategoryGroupResponse> Handle(CreateCategoryGroupCommand command, CancellationToken cancellationToken)
    {
        var count = await db.CategoryGroups.CountAsync(cancellationToken);

        var group = CategoryGroup.Create(
            name: command.Name,
            nameFr: command.NameFr,
            image: command.ImageUrl,
            isActive: command.IsActive,
            order: count + 1
        );

        db.Add(group);

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: group.Id
        );
    }
}