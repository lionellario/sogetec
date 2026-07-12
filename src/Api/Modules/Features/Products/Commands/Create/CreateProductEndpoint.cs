namespace Api.Modules.Features.Products.Commands.Create;

public sealed class CreateProductEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("products", CreateProductAsync)
            .ProducesPost<CreateProductResponse>()
            .WithTags(nameof(Product))
            .WithName(nameof(CreateProductEndpoint))
            .WithSummary("Create a new product.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Created<CreateProductResponse>> CreateProductAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] CreateProductCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        var path = linker.GetPathByName(nameof(CreateProductEndpoint), new { id = response.Id });

        return TypedResults.Created(path, response);
    }
}