namespace Api.Modules.Features.Brands.Commands.Create;

public record CreateBrandResponse(
    int Id,
    string Name,
    string? ImageUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateBrandCommand(
    int Id,
    string Name,
    string? ImageUrl
) : ICommand<CreateBrandResponse>;