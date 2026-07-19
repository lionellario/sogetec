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
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetCategoryByIdQuery(Guid Id) : IQuery<GetCategoryByIdResponse>;