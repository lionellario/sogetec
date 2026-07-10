namespace Api.Modules.Features.ProductAttributeHeaders.Commands.Create;

public sealed class CreateProductAttributeHeaderEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("product-attribute-headers", CreateProductAttributeHeaderAsync)
            .ProducesPost<CreateProductAttributeHeaderResponse>()
            .WithTags(nameof(ProductAttributeHeader))
            .WithName(nameof(CreateProductAttributeHeaderEndpoint))
            .WithSummary("Create a new product attribute header.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Created<CreateProductAttributeHeaderResponse>> CreateProductAttributeHeaderAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] CreateProductAttributeHeaderCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        var path = linker.GetPathByName(nameof(CreateProductAttributeHeaderEndpoint), new { id = response.Id });

        return TypedResults.Created(path, response);
    }
}