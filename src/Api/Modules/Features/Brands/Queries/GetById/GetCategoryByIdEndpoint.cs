namespace Api.Modules.Features.Brands.Queries.GetById;

public sealed class GetBrandByIdEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("brands/{brandId:int}", GetBrandByIdAsync)
            .ProducesGet<GetBrandByIdResponse>()
            .WithTags(nameof(Brand))
            .WithName(nameof(GetBrandByIdEndpoint))
            .WithSummary("Get a Brand using its internal Id.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<GetBrandByIdResponse>> GetBrandByIdAsync(
        ISender sender,
        int brandId,
        CancellationToken cancellationToken)
    {
        var query = new GetBrandByIdQuery(brandId);

        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}