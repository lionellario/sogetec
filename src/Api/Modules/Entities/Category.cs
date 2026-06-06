using Sogetec.Chassis.Domain;
using Sogetec.Chassis.Extensions;

namespace Api.Modules.Entities;

public sealed class Category : Entity, ISortableEntity
{
    public string Name { get; internal set; } = default!;
    public string Slug { get; internal set; } = default!;
    public Guid? ParentId { get; internal set; }
    public Category? Parent { get; internal set; }
    public string? Description { get; internal set; }
    public string? ImageUrl { get; internal set; }
    public bool IsActive { get; internal set; }
    public int SortOrder { get; set; }
    public ICollection<Category> SubCategories = [];

    public static Category Create(
        string name,
        Category? parent = null,
        string? description = null,
        string? image = null,
        int order = 0)
        => new()
        {
            Name = name,
            Slug = name.Slugify(),
            ParentId = parent?.Id,
            Parent = parent,
            Description = description,
            ImageUrl = image,
            IsActive = true,
            SortOrder = order
        };

    public override bool Equals(object? obj) => obj is Category at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();

    public void AssignParent(Category? parent)
    {
        if (IsAncestorOf(parent))
        {
            throw ConflictException.For<Category>(CategoryErrorCode.CategoryCannotBeSelfReferenced);
        }

        ParentId = parent?.Id;
        Parent = parent;
    }

    private bool IsAncestorOf(Category? category)
    {
        var current = category;
        while (current != null)
        {
            if (current == this) return true;
            current = current.Parent;
        }
        return false;
    }
}
