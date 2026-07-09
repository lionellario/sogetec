namespace Api.Modules.Features.Brands.Commands.Create;

public sealed class CreateBrandEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("brands", CreateBrandAsync)
            .ProducesPost<CreateBrandResponse>()
            .WithTags(nameof(Brand))
            .WithName(nameof(CreateBrandEndpoint))
            .WithSummary("Create a new Brand.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Created<CreateBrandResponse>> CreateBrandAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] CreateBrandCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        var path = linker.GetPathByName(nameof(CreateBrandEndpoint), new { id = response.Id });

        return TypedResults.Created(path, response);
    }
}