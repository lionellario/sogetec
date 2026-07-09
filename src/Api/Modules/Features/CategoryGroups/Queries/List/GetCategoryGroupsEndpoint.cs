namespace Api.Modules.Features.CategoryGroups.Queries.List;

public sealed class GetCategoryGroupsEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("category-groups", GetCategoryGroupsAsync)
            .ProducesGet<List<GetCategoryGroupRecord>>()
            .WithTags(nameof(Category))
            .WithName(nameof(GetCategoryGroupsEndpoint))
            .WithSummary("Get all Category Groups.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<List<GetCategoryGroupRecord>>> GetCategoryGroupsAsync(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryGroupsQuery();
        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}