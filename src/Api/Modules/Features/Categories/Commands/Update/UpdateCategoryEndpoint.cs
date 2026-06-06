namespace Api.Modules.Features.Categories.Commands.Update;

public sealed class UpdateCategoryEndpoint : IEndpoint
{
    public void Configure(IEndpointRouteBuilder app)
        => app
            .MapPost("categories/{categoryId:guid}", CreateCategoryAsync)
            .ProducesPut()
            .WithTags(nameof(Category))
            .WithName(nameof(UpdateCategoryEndpoint))
            .WithSummary("Update existing category.")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization(Authorize.Policies.Admin);

    public static async Task<Ok<UpdateCategoryResponse>> CreateCategoryAsync(
        ISender sender,
        Guid categoryId,
        [FromBody] UpdateCategoryCommand cmd,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(cmd, cancellationToken);

        return TypedResults.Ok(response);
    }
}