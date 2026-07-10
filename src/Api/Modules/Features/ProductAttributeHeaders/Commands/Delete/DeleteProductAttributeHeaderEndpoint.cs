namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Delete;

public sealed class DeleteProductSpecificationModelEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("product-attribute-headers/{headerId:int}", DeleteProductAttributeHeaderAsync)
            .ProducesDelete()
            .WithTags(nameof(ProductAttributeHeader))
            .WithName(nameof(DeleteProductSpecificationModelEndpoint))
            .WithSummary("Delete an existing product attribute header.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteProductAttributeHeaderAsync(
        ISender sender,
        int headerId,
        CancellationToken cancellationToken)
    {
        var cmd = new DeleteProductAttributeHeaderCommand(headerId);

        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}