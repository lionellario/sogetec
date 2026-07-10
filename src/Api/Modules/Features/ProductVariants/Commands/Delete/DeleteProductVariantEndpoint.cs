namespace Api.Modules.Features.ProductVariants.Commands.Delete;

public sealed class DeleteProductVariantEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("product-variants/{variantId:int}", DeleteProductVariantAsync)
            .ProducesDelete()
            .WithTags(nameof(ProductVariant))
            .WithName(nameof(DeleteProductVariantEndpoint))
            .WithSummary("Delete a new product variant.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteProductVariantAsync(
        ISender sender,
        int variantId,
        CancellationToken cancellationToken)
    {
        var cmd = new DeleteProductVariantCommand(variantId);

        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}