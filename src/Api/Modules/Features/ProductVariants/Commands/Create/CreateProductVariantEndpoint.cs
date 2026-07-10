namespace Api.Modules.Features.ProductVariants.Commands.Create;

public sealed class CreateProductVariantEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("product-variants", CreateProductVariantAsync)
            .ProducesPost<CreateProductVariantResponse>()
            .WithTags(nameof(ProductVariant))
            .WithName(nameof(CreateProductVariantEndpoint))
            .WithSummary("Create a new product variant.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Created<CreateProductVariantResponse>> CreateProductVariantAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] CreateProductVariantCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        var path = linker.GetPathByName(nameof(CreateProductVariantEndpoint), new { id = response.Id });

        return TypedResults.Created(path, response);
    }
}