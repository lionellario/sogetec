namespace Api.Modules.Features.Categories.Commands.Delete;

public sealed class DeleteCategoryEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapDelete("categories/{categoryId:guid}", DeleteCategoryAsync)
            .ProducesDelete()
            .WithTags(nameof(Category))
            .WithName(nameof(DeleteCategoryEndpoint))
            .WithSummary("Delete an existing category.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> DeleteCategoryAsync(
        ISender sender,
        Guid categoryId,
        CancellationToken cancellationToken)
    {
        var cmd = new DeleteCategoryCommand(categoryId);

        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}