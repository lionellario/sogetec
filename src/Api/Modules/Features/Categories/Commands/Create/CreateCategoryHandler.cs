namespace Api.Modules.Features.Categories.Commands.Create;

public sealed class CreateCategoryHandler(SogetecDbContext db) : ICommandHandler<CreateCategoryCommand, UpdateCategoryResponse>
{
    public async ValueTask<UpdateCategoryResponse> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var count = await db.Categories.CountAsync(cancellationToken);
        var parent = command.ParentId is null
                        ? null
                        : await db.Categories.FirstOrDefaultAsync(c => c.Id == command.ParentId, cancellationToken);

        if (command.ParentId is not null && parent is null)
        {
            throw new ValidationException([
                new ValidationFailure
                {
                    ErrorCode = CategoryErrorCode.ParentRequired.ToDisplayString()
                }
            ]);
        }

        var category = Category.Create(
            name: command.Name,
            parent: parent,
            description: command.Description,
            image: command.ImageUrl,
            order: count + 1
        );

        db.Add(category);

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: category.Id,
            Name: category.Name,
            Slug: category.Slug,
            ParentId: category.Parent?.Id,
            ParentName: category.Parent?.Name,
            Description: category.Description,
            IsActive: category.IsActive,
            ImageUrl: category.ImageUrl,
            SortOrder: category.SortOrder
        );
    }
}