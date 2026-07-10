namespace Api.Modules.Features.ProductAttributes.Commands.Create;

public sealed class CreateProductAttributeEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("product-attributes", CreateProductAttributeAsync)
            .ProducesPost<CreateProductAttributeResponse>()
            .WithTags(nameof(ProductAttribute))
            .WithName(nameof(CreateProductAttributeEndpoint))
            .WithSummary("Create a new product attribute.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Created<CreateProductAttributeResponse>> CreateProductAttributeAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] CreateProductAttributeCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        var path = linker.GetPathByName(nameof(CreateProductAttributeEndpoint), new { id = response.Id });

        return TypedResults.Created(path, response);
    }
}