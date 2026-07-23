namespace Api.Modules.Features.ProductItems.Commands.UpdateStatus;

public sealed class UpdateProductItemStatusEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("product-items/change-status", UpdateProductItemsStatusAsync)
            .ProducesPut()
            .WithTags(nameof(Product))
            .WithName(nameof(UpdateProductItemStatusEndpoint))
            .WithSummary("Update multiple product item status.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> UpdateProductItemsStatusAsync(
        ISender sender,
        [FromBody] UpdateProductItemStatusCommand cmd,
        CancellationToken cancellationToken)
    {
        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}