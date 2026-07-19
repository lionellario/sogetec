namespace Api.Modules.Features.CategoryGroups.Commands.Delete;

public sealed class DeleteCategoryGroupEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("category-groups/{groupId:guid}", DeleteCategoryGroupAsync)
            .ProducesDelete()
            .WithTags(nameof(Category))
            .WithName(nameof(DeleteCategoryGroupEndpoint))
            .WithSummary("Delete a new category group.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteCategoryGroupAsync(
        ISender sender,
        Guid groupId,
        CancellationToken cancellationToken)
    {
        var cmd = new DeleteCategoryGroupCommand(groupId);

        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}