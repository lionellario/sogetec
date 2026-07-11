namespace Api.Modules.Features.ProductItems.Queries.GetById;

public sealed class GetProductItemByIdEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("product-items/{productItemId:int}", GetProductItemByIdAsync)
            .ProducesGet<GetProductItemByIdResponse>()
            .WithTags(nameof(ProductItem))
            .WithName(nameof(GetProductItemByIdEndpoint))
            .WithSummary("Get a product item using its internal Id.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<GetProductItemByIdResponse>> GetProductItemByIdAsync(
        ISender sender,
        int ProductItemId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductItemByIdQuery(ProductItemId);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}