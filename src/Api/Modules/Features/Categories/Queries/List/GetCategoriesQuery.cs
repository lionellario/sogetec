namespace Api.Modules.Features.Categories.Queries.List;

public record GetCategoryDto(
    int Id,
    string Name,
    string Slug,
    int? ParentId,
    string? ParentName
);

public record GetCategoriesQuery : IQuery<List<GetCategoryDto>>;
