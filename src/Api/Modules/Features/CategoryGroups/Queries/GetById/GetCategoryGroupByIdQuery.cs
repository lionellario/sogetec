namespace Api.Modules.Features.CategoryGroups.Queries.GetById;

public record GetCategoryGroupByIdResponse(
    int Id,
    string Name,
    bool IsActive,
    string ImageUrl,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetCategoryGroupByIdQuery(int Id) : IQuery<GetCategoryGroupByIdResponse>;