namespace Api.Modules.Features.ProductAttributeHeaders.Queries.List;

public sealed class GetProductSpecificationModelsEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("product-attribute-headers", GetProductSpecificationModelsAsync)
            .ProducesGet<List<GetProductSpecificationModelRecord>>()
            .WithTags(nameof(ProductAttributeHeader))
            .WithName(nameof(GetProductSpecificationModelsEndpoint))
            .WithSummary("Get all product attribute header.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<List<GetProductSpecificationModelRecord>>> GetProductSpecificationModelsAsync(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetProductSpecificationModelsQuery();
        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}