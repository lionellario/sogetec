namespace Api.Modules.Features.CategoryGroups.Queries.GetById;

public sealed class GetCategoryGroupByIdEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("category-groups/{groupId:int}", GetCategoryGroupByIdAsync)
            .ProducesGet<GetCategoryGroupByIdResponse>()
            .WithTags(nameof(CategoryGroup))
            .WithName(nameof(GetCategoryGroupByIdEndpoint))
            .WithSummary("Get a category group using its internal Id.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<GetCategoryGroupByIdResponse>> GetCategoryGroupByIdAsync(
        ISender sender,
        int groupId,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryGroupByIdQuery(groupId);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}