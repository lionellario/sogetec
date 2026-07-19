namespace Api.Modules.Features.Brands.Commands.Update;

public record UpdateBrandResponse(
    Guid Id,
    string Name,
    string? ImageUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset LastModifiedAt
);

public record UpdateBrandCommand(
    Guid Id,
    string Name,
    string? ImageUrl
) : ICommand<UpdateBrandResponse>;