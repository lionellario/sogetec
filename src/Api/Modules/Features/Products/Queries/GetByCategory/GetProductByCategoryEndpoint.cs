namespace Api.Modules.Features.Products.Queries.GetByCategory;

public sealed class GetProductByCategoryEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("products/categories/{categoryId:int}", GetProductByCategoryAsync)
            .ProducesGet<List<ProductByCategoryRecord>>()
            .WithTags(nameof(Product))
            .WithName(nameof(GetProductByCategoryEndpoint))
            .WithSummary("Get a product using its internal Id.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<List<ProductByCategoryRecord>>> GetProductByCategoryAsync(
        ISender sender,
        int categoryId,
        bool includeInactiveProducts,
        CancellationToken cancellationToken)
    {
        var query = new GetProductByCategoryQuery(categoryId, includeInactiveProducts);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}