namespace Api.Modules.Features.ProductAttributes.Queries.List;

public sealed class GetProductAttributesEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("product-attributes", GetProductAttributesAsync)
            .ProducesGet<List<GetProductAttributeRecord>>()
            .WithTags(nameof(ProductAttribute))
            .WithName(nameof(GetProductAttributesEndpoint))
            .WithSummary("Get all product attributes.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<List<GetProductAttributeRecord>>> GetProductAttributesAsync(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetProductAttributesQuery();
        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}