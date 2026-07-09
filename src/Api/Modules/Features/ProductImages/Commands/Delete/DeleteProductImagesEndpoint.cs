namespace Api.Modules.Features.ProductImages.Commands.Delete;

public sealed class DeleteProductImagesEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("product-images", DeleteProductImagesAsync)
            .ProducesDelete()
            .WithTags(nameof(Category))
            .WithName(nameof(DeleteProductImagesEndpoint))
            .WithSummary("Delete multiple images for a product.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteProductImagesAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] DeleteProductImagesCommand cmd,
        CancellationToken cancellationToken)
    {
        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}