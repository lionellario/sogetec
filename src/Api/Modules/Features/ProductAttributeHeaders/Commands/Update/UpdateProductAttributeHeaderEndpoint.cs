namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Update;

public sealed class UpdateProductAttributeHeaderEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("product-attribute-headers/{headerId:int}", UpdateProductAttributeHeaderAsync)
            .ProducesPut()
            .WithTags(nameof(ProductAttributeHeader))
            .WithName(nameof(UpdateProductAttributeHeaderEndpoint))
            .WithSummary("Update existing product attribute header.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Ok<UpdateProductAttributeHeaderResponse>> UpdateProductAttributeHeaderAsync(
        ISender sender,
        int headerId,
        [FromBody] UpdateProductAttributeHeaderCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        return TypedResults.Ok(response);
    }
}