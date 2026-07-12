namespace Api.Modules.Features.Categories.Commands.Create;

public sealed class CreateCategoryHandler(SogetecDbContext db) : ICommandHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    public async ValueTask<CreateCategoryResponse> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var count = await db.Categories.CountAsync(cancellationToken);
        var parent = command.ParentId is null
                        ? null
                        : await db.Categories
                            .Include(c => c.Group)
                            .FirstOrDefaultAsync(c => c.Id == command.ParentId, cancellationToken);

        var desiredGroup = await db.CategoryGroups.FirstOrDefaultAsync(g => g.Id == command.GroupId, cancellationToken);

        if (command.ParentId is not null && parent is null)
        {
            throw new ValidationException([
                new ValidationFailure
                {
                    ErrorCode = CategoryErrorCode.ParentRequired.ToDisplayString()
                }
            ]);
        }

        var group = parent?.Group ?? desiredGroup;

        if (group is null)
        {
            throw NotFoundException.For<CategoryGroup>(
                command.GroupId,
                CategoryErrorCode.CategoryGroupNotFound);
        }

        var category = Category.Create(
            name: command.Name,
            nameFr: command.NameFr,
            group: group,
            parent: parent,
            description: command.Description,
            image: command.ImageUrl,
            isActive: command.IsActive,
            order: count + 1
        );

        db.Add(category);

        if(group.Categories.Count > 2)
        {
            throw new ValidationException([
                new ValidationFailure
                {
                    ErrorCode = CategoryErrorCode.GroupMaxCategoryExceeded.ToDisplayString()
                }
            ]);
        }

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: category.Id,
            Name: category.Name,
            NameFr: category.NameFr,
            Slug: category.Slug,
            GroupId: category.GroupId,
            GroupName: group.Name,
            ParentId: category.Parent?.Id,
            ParentName: category.Parent?.Name,
            Description: category.Description,
            IsActive: category.IsActive,
            ImageUrl: category.ImageUrl,
            SortOrder: category.SortOrder,
            CreatedAt: category.CreatedOn,
            LastModifiedAt: category.LastModifiedOn
        );
    }
}