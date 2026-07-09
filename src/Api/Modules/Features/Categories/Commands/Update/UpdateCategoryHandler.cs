using Sogetec.Chassis.Extensions;

namespace Api.Modules.Features.Categories.Commands.Update;

public sealed class GetCategoryByIdHandler(SogetecDbContext db) : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    public async ValueTask<UpdateCategoryResponse> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var desiredParentId = command.ParentId;

        var family = await (
            // Load the target category
            from target in db.Categories.Where(targetCategory => targetCategory.Id == command.Id && targetCategory.IsActive)

            // Load the family
            from candidate in db.Categories
                .Include(x => x.Group)
                .Where(candidateCategory => candidateCategory.IsActive && (
                    // load the target
                    candidateCategory.Id == target.Id ||

                    // load the desired parent
                    (desiredParentId.HasValue && candidateCategory.Id == desiredParentId.Value) ||

                    // Load the siblings of the target
                    (candidateCategory.ParentId == target.ParentId && target.ParentId != null) ||

                    // future siblings of the target category (categories that already live under the new desired parent)
                    (target.ParentId != desiredParentId && candidateCategory.ParentId == desiredParentId)))
            select new
            {
                Category = candidate,
                IsTarget = candidate.Id == target.Id
            })
            .ToListAsync(cancellationToken);

        var desiredGroup = await db.CategoryGroups.FirstOrDefaultAsync(g => g.Id == command.GroupId, cancellationToken);

        var category = family.FirstOrDefault(x => x.IsTarget)?.Category;

        if (category is null)
        {
            throw NotFoundException.For<Category>(
                command.Id,
                CategoryErrorCode.CategoryNotFound);
        }

        var oldParentId = category.ParentId;
        var parent = family.Select(x => x.Category).FirstOrDefault(n => n.Id == desiredParentId);

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

        category.Name = command.Name;
        category.Slug = command.Name.Slugify();
        category.Description = command.Description;
        category.ImageUrl = command.ImageUrl;
        category.Group = group;
        category.GroupId = group.Id;
        category.IsActive = command.IsActive;
        category.AssignParent(parent);

        var familyCategories = family.Select(x => x.Category);
        var parentChanged = oldParentId != desiredParentId;

        if (parentChanged)
        {
            var oldSiblings = familyCategories.Where(n => n.ParentId == oldParentId && n.Id != category.Id).ToList();

            SortOrderChain.Normalize(oldSiblings);

            var newSiblings = familyCategories.Where(n => n.ParentId == desiredParentId && n.Id != category.Id).ToList();

            SortOrderChain.PlaceInChain(category, newSiblings, command.SortOrder);
        }
        else if (command.SortOrder.HasValue)
        {
            var siblings = familyCategories.Where(n => n.ParentId == desiredParentId).ToList();
            SortOrderChain.PlaceInChain(category, siblings, command.SortOrder);
        }

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: category.Id,
            Name: category.Name,
            Slug: category.Slug,
            GroupId: category.GroupId,
            GroupName: category.Group.Name,
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