namespace Api.Modules.Features.ProductAttributeHeaders.Queries.GetById;

public sealed class GetProductAttributeHeaderByIdEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("product-attribute-headers/{headerId:int}", GetProductAttributeHeaderByIdAsync)
            .ProducesGet<GetProductAttributeHeaderByIdResponse>()
            .WithTags(nameof(ProductAttributeHeader))
            .WithName(nameof(GetProductAttributeHeaderByIdEndpoint))
            .WithSummary("Get a product attribute header using its internal Id.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<GetProductAttributeHeaderByIdResponse>> GetProductAttributeHeaderByIdAsync(
        ISender sender,
        int headerId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductAttributeHeaderByIdQuery(headerId);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}