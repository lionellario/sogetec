namespace Api.Modules.Features.Products.Commands.Delete;

public sealed class DeleteProductEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("products/{productId:int}", DeleteProductAsync)
            .ProducesDelete()
            .WithTags(nameof(Product))
            .WithName(nameof(DeleteProductEndpoint))
            .WithSummary("Delete a product.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteProductAsync(
        ISender sender,
        int productId,
        CancellationToken cancellationToken)
    {
        var cmd = new DeleteProductCommand(productId);

        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}