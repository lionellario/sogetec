namespace Api.Modules.Features.ProductItems.Commands.Create;

public sealed class CreateProductItemEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("product-items", CreateProductItemAsync)
            .ProducesPost<CreateProductItemResponse>()
            .WithTags(nameof(ProductItem))
            .WithName(nameof(CreateProductItemEndpoint))
            .WithSummary("Create a new product item.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Created<CreateProductItemResponse>> CreateProductItemAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] CreateProductItemCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        var path = linker.GetPathByName(nameof(CreateProductItemEndpoint), new { id = response.Id });

        return TypedResults.Created(path, response);
    }
}