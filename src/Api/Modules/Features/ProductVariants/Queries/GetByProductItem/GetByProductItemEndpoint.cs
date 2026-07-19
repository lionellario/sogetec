namespace Api.Modules.Features.ProductVariants.Queries.GetByProductItem;

public sealed class GetByProductItemsEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("product-variants/product-items/{productItemId:guid}", GetByProductItemsAsync)
            .ProducesGet<List<GetByProductItemRecord>>()
            .WithTags(nameof(GetByProductItem))
            .WithName(nameof(GetByProductItemsEndpoint))
            .WithSummary("Get all product variants by product item.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<List<GetByProductItemRecord>>> GetByProductItemsAsync(
        ISender sender,
        Guid productItemId,
        CancellationToken cancellationToken)
    {
        var query = new GetByProductItemQuery(productItemId);
        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}