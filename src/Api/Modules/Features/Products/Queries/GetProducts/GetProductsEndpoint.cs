using Sogetec.Chassis.Pagination;

namespace Api.Modules.Features.Products.Queries.GetProducts;

public sealed class GetProductsEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("products", GetProductByCategoryAsync)
            .ProducesGet<PagedResponse<ProductRecord>>()
            .WithTags(nameof(Product))
            .WithName(nameof(GetProductsEndpoint))
            .WithSummary("Get products.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<PagedResponse<ProductRecord>>> GetProductByCategoryAsync(
        ISender sender,
        int? pageNumber,
        int? pageSize,
        CancellationToken cancellationToken)
    {
        var pagination = new PaginationQueryFilter(pageIndex: pageNumber, pageSize: pageSize);
        var query = new GetProductsQuery(pagination);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}