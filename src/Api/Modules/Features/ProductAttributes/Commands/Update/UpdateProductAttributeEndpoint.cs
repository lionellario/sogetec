namespace Api.Modules.Features.ProductAttributes.Commands.Update;

public sealed class UpdateProductAttributeEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("product-attributes/{attributeId:int}", UpdateProductAttributeAsync)
            .ProducesPut()
            .WithTags(nameof(ProductAttribute))
            .WithName(nameof(UpdateProductAttributeEndpoint))
            .WithSummary("Update existing product attribute.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Ok<UpdateProductAttributeResponse>> UpdateProductAttributeAsync(
        ISender sender,
        int attributeId,
        [FromBody] UpdateProductAttributeCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        return TypedResults.Ok(response);
    }
}