namespace Api.Modules.Features.CategoryGroups.Commands.Delete;

public sealed class DeleteCategoryGroupEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("category-groups", DeleteCategoryGroupAsync)
            .ProducesDelete()
            .WithTags(nameof(Category))
            .WithName(nameof(DeleteCategoryGroupEndpoint))
            .WithSummary("Delete multiple category groups.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteCategoryGroupAsync(
        ISender sender,
        [FromBody] DeleteCategoryGroupCommand cmd,
        CancellationToken cancellationToken)
    {
        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}