namespace Api.Modules.Features.ProductAttributes.Commands.Delete;

public sealed class DeleteProductAttributeEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("product-attributes/{attributeId:int}", DeleteProductAttributeAsync)
            .ProducesDelete()
            .WithTags(nameof(ProductAttribute))
            .WithName(nameof(DeleteProductAttributeEndpoint))
            .WithSummary("Delete an existing product attribute.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteProductAttributeAsync(
        ISender sender,
        int attributeId,
        CancellationToken cancellationToken)
    {
        var cmd = new DeleteProductAttributeCommand(attributeId);

        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}