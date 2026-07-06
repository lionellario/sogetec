namespace Api.Modules.Features.Categories.Queries.List;

public sealed class GetCategoriesEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("categories", GetCategoriesAsync)
            .ProducesGet<List<GetCategoryDto>>()
            .WithTags(nameof(Category))
            .WithName(nameof(GetCategoriesEndpoint))
            .WithSummary("Get all categories.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<List<GetCategoryDto>>> GetCategoriesAsync(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoriesQuery();
        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}