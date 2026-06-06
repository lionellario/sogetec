namespace Api.Modules.Features.Categories.Commands.Create;

public sealed class CreateCategoryEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("categories", CreateCategoryAsync)
            .ProducesPost<UpdateCategoryResponse>()
            .WithTags(nameof(Category))
            .WithName(nameof(CreateCategoryEndpoint))
            .WithSummary("Create a new category.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Created<UpdateCategoryResponse>> CreateCategoryAsync(
        ISender sender,
        LinkGenerator linker,
        [FromBody] CreateCategoryCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        var path = linker.GetPathByName(nameof(CreateCategoryEndpoint), new { id = response.Id });

        return TypedResults.Created(path, response);
    }
}