namespace Api.Modules.Features.CategoryGroups.Commands.UpdateStatus;

public sealed class UpdateCategoryGroupStatusEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("category-groups/change-status", UpdateCategoryGroupStatusAsync)
            .ProducesPut()
            .WithTags(nameof(CategoryGroup))
            .WithName(nameof(UpdateCategoryGroupStatusEndpoint))
            .WithSummary("Update multiple category group status.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<NoContent> UpdateCategoryGroupStatusAsync(
        ISender sender,
        [FromBody] UpdateCategoryGroupStatusCommand cmd,
        CancellationToken cancellationToken)
    {
        await sender.Send(cmd, cancellationToken);

        return TypedResults.NoContent();
    }
}