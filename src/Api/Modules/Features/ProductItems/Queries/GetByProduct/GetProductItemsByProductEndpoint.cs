namespace Api.Modules.Features.ProductItems.Queries.GetByProduct;

public sealed class GetProductItemsByProductEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("product-items/products/{productId:guid}", GetCategoriesAsync)
            .ProducesGet<List<GetProductItemByIdRecord>>()
            .WithTags(nameof(ProductItem))
            .WithName(nameof(GetProductItemsByProductEndpoint))
            .WithSummary("Get product items for a product.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<List<GetProductItemByIdRecord>>> GetCategoriesAsync(
        ISender sender,
        Guid productId,
        bool? includeInactive,
        CancellationToken cancellationToken)
    {
        var query = new GetProductItemsByProductQuery(productId, includeInactive);
        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}