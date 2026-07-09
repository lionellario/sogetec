namespace Api.Modules.Features.CategoryGroups.Commands.Create;

public sealed class CreateCategoryGroupEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("category-groups", CreateCategoryGroupAsync)
            .ProducesPost<CreateCategoryGroupResponse>()
            .WithTags(nameof(Category))
            .WithName(nameof(CreateCategoryGroupEndpoint))
            .WithSummary("Create a new category group.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Created<CreateCategoryGroupResponse>> CreateCategoryGroupAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] CreateCategoryGroupCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        var path = linker.GetPathByName(nameof(CreateCategoryGroupEndpoint), new { id = response.Id });

        return TypedResults.Created(path, response);
    }
}