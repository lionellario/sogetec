namespace Api.Modules.Features.Brands.Commands.Create;

public record CreateBrandResponse(
    Guid Id,
    string Name,
    string? ImageUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record CreateBrandCommand(
    Guid Id,
    string Name,
    string? ImageUrl
) : ICommand<CreateBrandResponse>;