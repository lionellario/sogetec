namespace Api.Modules.Features.Products.Queries.GetById;

public record GetProductByIdResponse(
    int Id,
    string Name,
    string NameFr,
    string Slug,
    string Description,
    bool IsActive
);

public record GetProductByIdQuery(int Id) : IQuery<GetProductByIdResponse>;
