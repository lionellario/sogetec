namespace Api.Modules.Features.CategoryGroups.Commands.Update;

public sealed class UpdateCategoryGroupEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("category-groups/{groupId:guid}", UpdateCategoryGroupAsync)
            .ProducesPut()
            .WithTags(nameof(Category))
            .WithName(nameof(UpdateCategoryGroupEndpoint))
            .WithSummary("Update a new category group.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Ok<UpdateCategoryGroupResponse>> UpdateCategoryGroupAsync(
        ISender sender,
        Guid groupId,
        [FromBody] UpdateCategoryGroupCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        return TypedResults.Ok(response);
    }
}