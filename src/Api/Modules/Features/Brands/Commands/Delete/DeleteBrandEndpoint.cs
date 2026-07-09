namespace Api.Modules.Features.Brands.Commands.Delete;

public sealed class DeleteBrandEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("Brands/{brandId:int}", DeleteBrandAsync)
            .ProducesDelete()
            .WithTags(nameof(Brand))
            .WithName(nameof(DeleteBrandEndpoint))
            .WithSummary("Delete an existing Brand.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteBrandAsync(
        ISender sender,
        int brandId,
        CancellationToken cancellationToken)
    {
        var cmd = new DeleteBrandCommand(brandId);

        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}