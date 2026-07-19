namespace Api.Modules.Features.Products.Queries.GetById;

public sealed class GetProductByIdEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("products/{productId:guid}", GetProductByIdAsync)
            .ProducesGet<GetProductByIdResponse>()
            .WithTags(nameof(Product))
            .WithName(nameof(GetProductByIdEndpoint))
            .WithSummary("Get a product using its internal Id.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<GetProductByIdResponse>> GetProductByIdAsync(
        ISender sender,
        Guid productId,
        CancellationToken cancellationToken)
    {
        var query = new GetProductByIdQuery(productId);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}