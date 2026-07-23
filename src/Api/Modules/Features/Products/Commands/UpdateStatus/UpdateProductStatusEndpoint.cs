namespace Api.Modules.Features.Products.Commands.UpdateStatus;

public sealed class UpdateProductStatusEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("products/change-status", UpdateProductStatusAsync)
            .ProducesPut()
            .WithTags(nameof(Product))
            .WithName(nameof(UpdateProductStatusEndpoint))
            .WithSummary("Update multiple product status.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> UpdateProductStatusAsync(
        ISender sender,
        [FromBody] UpdateProductStatusCommand cmd,
        CancellationToken cancellationToken)
    {
        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}