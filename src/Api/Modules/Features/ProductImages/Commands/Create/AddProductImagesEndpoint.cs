namespace Api.Modules.Features.ProductImages.Commands.Create;

public sealed class AddProductImagesEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("product-images", AddProductImagesAsync)
            .ProducesPost<List<AddProductImagesRecord>>()
            .WithTags(nameof(Category))
            .WithName(nameof(AddProductImagesEndpoint))
            .WithSummary("Add multiple images for a product.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Created<List<AddProductImagesRecord>>> AddProductImagesAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] AddProductImagesCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        var path = linker.GetPathByName(nameof(AddProductImagesEndpoint));

        return TypedResults.Created(path, response);
    }
}