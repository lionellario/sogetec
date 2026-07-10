namespace Api.Modules.Features.ProductVariants.Commands.Update;

public sealed class UpdateProductVariantEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("product-variants/{variantId:int}", UpdateProductVariantAsync)
            .ProducesPut()
            .WithTags(nameof(ProductVariant))
            .WithName(nameof(UpdateProductVariantEndpoint))
            .WithSummary("Update existing product variant.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Ok<UpdateProductVariantResponse>> UpdateProductVariantAsync(
        ISender sender,
        int variantId,
        [FromBody] UpdateProductVariantCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        return TypedResults.Ok(response);
    }
}