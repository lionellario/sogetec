namespace Api.Modules.Features.ProductItems.Commands.Delete;

public sealed class CreateProductItemEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("product-items/{productItemId:int}", CreateProductItemAsync)
            .ProducesDelete()
            .WithTags(nameof(ProductItem))
            .WithName(nameof(CreateProductItemEndpoint))
            .WithSummary("Create a new product item.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> CreateProductItemAsync(
        ISender sender,
        int productItemId,
        CancellationToken cancellationToken)
    {
        var cmd = new DeleteProductItemCommand(productItemId);

        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}