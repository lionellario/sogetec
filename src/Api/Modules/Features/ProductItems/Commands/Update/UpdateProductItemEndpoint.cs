namespace Api.Modules.Features.ProductItems.Commands.Update;

public sealed class UpdateProductItemEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("product-items/{productItemId:int}", UpdateProductItemAsync)
            .ProducesPut()
            .WithTags(nameof(ProductItem))
            .WithName(nameof(UpdateProductItemEndpoint))
            .WithSummary("Update existing product item.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Ok<UpdateProductItemResponse>> UpdateProductItemAsync(
        ISender sender,
        int productItemId,
        [FromBody] UpdateProductItemCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        return TypedResults.Ok(response);
    }
}