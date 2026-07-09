namespace Api.Modules.Features.Brands.Commands.Create;

public sealed class CreateBrandHandler(SogetecDbContext db) : ICommandHandler<CreateBrandCommand, CreateBrandResponse>
{
    public async ValueTask<CreateBrandResponse> Handle(CreateBrandCommand command, CancellationToken cancellationToken)
    {
        var brand = Brand.Create(
            name: command.Name,
            logoUrl: command.ImageUrl ?? "fake-logo.png"
        );

        db.Add(brand);

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: brand.Id,
            Name: brand.Name,
            ImageUrl: brand.LogoUrl,
            CreatedAt: brand.CreatedOn,
            LastModifiedAt: brand.LastModifiedOn
        );
    }
}