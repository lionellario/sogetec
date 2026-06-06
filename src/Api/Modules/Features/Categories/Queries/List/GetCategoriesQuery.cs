namespace Api.Modules.Features.Categories.Queries.List;

public record GetCategoryDto(
    Guid Id,
    string Name,
    string Slug,
    Guid? ParentId,
    string? ParentName
);

public record GetCategoriesQuery : IQuery<List<GetCategoryDto>>;
