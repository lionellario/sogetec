namespace Api.Modules.Features.Categories.Queries.GetById;

public record GetCategoryByIdResponse(
    Guid Id,
    string Name,
    string Slug,
    Guid? ParentId,
    string? ParentName,
    string? Description,
    bool IsActive,
    string? ImageUrl,
    int SortOrder
);

public record GetCategoryByIdQuery(Guid Id) : IQuery<GetCategoryByIdResponse>;