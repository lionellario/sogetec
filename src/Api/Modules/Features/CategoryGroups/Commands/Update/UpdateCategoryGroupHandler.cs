using Sogetec.Chassis.Extensions;

namespace Api.Modules.Features.CategoryGroups.Commands.Update;

public sealed class UpdateCategoryGroupHandler(SogetecDbContext db) : ICommandHandler<UpdateCategoryGroupCommand, UpdateCategoryGroupResponse>
{
    public async ValueTask<UpdateCategoryGroupResponse> Handle(UpdateCategoryGroupCommand command, CancellationToken cancellationToken)
    {
        var groups = await db.CategoryGroups.ToListAsync(cancellationToken);

        var group = groups.FirstOrDefault(g => g.Id == command.Id);

        if (group is null)
        {
            throw NotFoundException.For<CategoryGroup>(
                command.Id,
                CategoryErrorCode.CategoryGroupNotFound);
        }

        group.Name = command.Name;
        group.ImageUrl = command.ImageUrl;
        group.IsActive = command.IsActive;

        if (group.SortOrder != command.SortOrder)
        {
            var siblings = groups.Where(g => g.Id != command.Id);
            SortOrderChain.PlaceInChain(group, siblings, command.SortOrder);
        }

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: group.Id,
            Name: group.Name,
            ImageUrl: group.ImageUrl,
            SortOrder: group.SortOrder,
            IsActive: group.IsActive,
            CreatedAt: group.CreatedOn,
            LastModifiedAt: group.LastModifiedOn
        );
    }
}