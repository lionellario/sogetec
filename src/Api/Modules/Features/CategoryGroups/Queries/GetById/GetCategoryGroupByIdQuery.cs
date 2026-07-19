namespace Api.Modules.Features.CategoryGroups.Queries.GetById;

public record GetCategoryGroupByIdResponse(
    Guid Id,
    string Name,
    bool IsActive,
    string ImageUrl,
    int SortOrder,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record GetCategoryGroupByIdQuery(Guid Id) : IQuery<GetCategoryGroupByIdResponse>;