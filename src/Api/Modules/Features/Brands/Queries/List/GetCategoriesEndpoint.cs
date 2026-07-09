namespace Api.Modules.Features.Brands.Queries.List;

public sealed class GetBrandsEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapGet("brands", GetBrandsAsync)
            .ProducesGet<List<GetBrandRecord>>()
            .WithTags(nameof(Brand))
            .WithName(nameof(GetBrandsEndpoint))
            .WithSummary("Get all brands.")
            .MapToApiVersion(ApiVersions.V1);

    public static async Task<Ok<List<GetBrandRecord>>> GetBrandsAsync(
        ISender sender,
        CancellationToken cancellationToken)
    {
        var query = new GetBrandsQuery();
        var response = await sender.Send(query, cancellationToken);

        return TypedResults.Ok(response);
    }
}