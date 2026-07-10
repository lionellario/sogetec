namespace Api.Modules.Features.Categories.Queries.GetById;

public record GetCategoryByIdResponse(
    int Id,
    string Name,
    string Slug,
    int? ParentId,
    string? ParentName,
    string? Description,
    bool IsActive,
    string? ImageUrl,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetCategoryByIdQuery(int Id) : IQuery<GetCategoryByIdResponse>;