namespace Api.Modules.Features.Categories.Queries.GetById;

public sealed class GetCategoryByIdEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("categories/{categoryId:guid}", GetCategoryByIdAsync)
            .ProducesGet<GetCategoryByIdResponse>()
            .WithTags(nameof(Category))
            .WithName(nameof(GetCategoryByIdEndpoint))
            .WithSummary("Update existing category.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

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