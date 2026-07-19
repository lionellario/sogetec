using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Products.Queries.GetByCategory;

public sealed class GetProductByCategoryEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("products/categories/{categoryId:int}", GetProductByCategoryAsync)
            .ProducesGet<PagedResponse<ProductByCategoryRecord>>()
            .WithTags(nameof(Product))
            .WithName(nameof(GetProductByCategoryEndpoint))
            .WithSummary("Get a list of product for a given category.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<PagedResponse<ProductByCategoryRecord>>> GetProductByCategoryAsync(
        ISender sender,
        int categoryId,
        int? pageNumber,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var pagination = new PaginationQueryFilter(pageIndex: pageNumber, pageSize: pageSize);
        var query = new GetProductByCategoryQuery(categoryId, pagination);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}