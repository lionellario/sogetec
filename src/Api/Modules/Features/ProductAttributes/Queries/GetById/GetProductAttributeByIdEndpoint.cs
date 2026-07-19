namespace Api.Modules.Features.ProductAttributes.Queries.GetById;

public sealed class GetProductAttributeByIdEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("product-attributes/{attributeId:guid}", GetProductAttributeByIdAsync)
            .ProducesGet<GetProductAttributeByIdResponse>()
            .WithTags(nameof(ProductAttribute))
            .WithName(nameof(GetProductAttributeByIdEndpoint))
            .WithSummary("Get a product attribute using its internal Id.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<GetProductAttributeByIdResponse>> GetProductAttributeByIdAsync(
        ISender sender,
        Guid attributeId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductAttributeByIdQuery(attributeId);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}