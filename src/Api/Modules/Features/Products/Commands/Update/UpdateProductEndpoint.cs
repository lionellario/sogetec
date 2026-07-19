namespace Api.Modules.Features.Products.Commands.Update;

public sealed class UpdateProductEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("products/{productId:guid}", UpdateProductAsync)
            .ProducesPut()
            .WithTags(nameof(Product))
            .WithName(nameof(UpdateProductEndpoint))
            .WithSummary("Update a product.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Ok<UpdateProductResponse>> UpdateProductAsync(
        ISender sender,
        Guid productId,
        [FromBody] UpdateProductCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        return TypedResults.Ok(response);
    }
}