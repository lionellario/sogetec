namespace Api.Modules.Features.ProductItems.Commands.Delete;

public sealed class DeleteProductItemEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("product-items/{productItemId:guid}", CreateProductItemAsync)
            .ProducesDelete()
            .WithTags(nameof(ProductItem))
            .WithName(nameof(DeleteProductItemEndpoint))
            .WithSummary("Delete a product item.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> CreateProductItemAsync(
        ISender sender,
        Guid productItemId,
        CancellationToken cancellationToken)
    {
        var cmd = new DeleteProductItemCommand(productItemId);

        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}