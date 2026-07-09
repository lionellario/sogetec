namespace Api.Modules.Features.Brands.Commands.Update;

public record UpdateBrandResponse(
    int Id,
    string Name,
    string? ImageUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateBrandCommand(
    int Id,
    string Name,
    string? ImageUrl
) : ICommand<UpdateBrandResponse>;