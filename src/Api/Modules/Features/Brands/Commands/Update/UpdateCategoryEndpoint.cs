namespace Api.Modules.Features.Brands.Commands.Update;

public sealed class UpdateBrandEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPut("brands/{brandId:int}", UpdateBrandAsync)
            .ProducesPut()
            .WithTags(nameof(Brand))
            .WithName(nameof(UpdateBrandEndpoint))
            .WithSummary("Update existing brand.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Ok<UpdateBrandResponse>> UpdateBrandAsync(
        ISender sender,
        int brandId,
        [FromBody] UpdateBrandCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        return TypedResults.Ok(response);
    }
}