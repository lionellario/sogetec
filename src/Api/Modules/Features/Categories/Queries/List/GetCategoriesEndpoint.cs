using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Categories.Queries.List;

public sealed class GetCategoriesEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("categories", GetCategoriesAsync)
            .ProducesGet<PagedResponse<GetCategoryRecord>>()
            .WithTags(nameof(Category))
            .WithName(nameof(GetCategoriesEndpoint))
            .WithSummary("Get all categories.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<PagedResponse<GetCategoryRecord>>> GetCategoriesAsync(
        ISender sender,
        int? pageNumber,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var pagination = new PaginationQueryFilter(pageIndex: pageNumber, pageSize: pageSize);
        var query = new GetCategoriesQuery(pagination);
        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}