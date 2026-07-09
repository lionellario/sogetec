namespace Api.Modules.Features.CategoryGroups.Queries.GetById;

public sealed class GetCategoryByIdEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("category-groups/{groupId:int}", GetCategoryByIdAsync)
            .ProducesGet<GetCategoryGroupByIdResponse>()
            .WithTags(nameof(Category))
            .WithName(nameof(GetCategoryByIdEndpoint))
            .WithSummary("Get a Category Group using its internal Id.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<GetCategoryGroupByIdResponse>> GetCategoryByIdAsync(
        ISender sender,
        int groupId,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryGroupByIdQuery(groupId);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}