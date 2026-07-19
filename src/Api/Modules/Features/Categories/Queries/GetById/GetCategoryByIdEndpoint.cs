namespace Api.Modules.Features.Categories.Queries.GetById;

public sealed class GetCategoryByIdEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("categories/{categoryId:guid}", GetCategoryByIdAsync)
            .ProducesGet<GetCategoryByIdResponse>()
            .WithTags(nameof(Category))
            .WithName(nameof(GetCategoryByIdEndpoint))
            .WithSummary("Get a Category using its internal Id.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<GetCategoryByIdResponse>> GetCategoryByIdAsync(
        ISender sender,
        Guid categoryId,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryByIdQuery(categoryId);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}