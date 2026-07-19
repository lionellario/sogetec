namespace Api.Modules.Features.Brands.Commands.Delete;

public sealed class DeleteBrandEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("Brands", DeleteBrandAsync)
            .ProducesDelete()
            .WithTags(nameof(Brand))
            .WithName(nameof(DeleteBrandEndpoint))
            .WithSummary("Delete an existing Brand.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteBrandAsync(
        ISender sender,
        [FromBody] DeleteBrandCommand cmd,
        CancellationToken cancellationToken)
    {
        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}