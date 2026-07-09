using Sogetec.Chassis.Extensions;

namespace Api.Modules.Features.Brands.Commands.Update;

public sealed class GetBrandByIdHandler(SogetecDbContext db) : ICommandHandler<UpdateBrandCommand, UpdateBrandResponse>
{
    public async ValueTask<UpdateBrandResponse> Handle(UpdateBrandCommand command, CancellationToken cancellationToken)
    {
        var brand = db.Brands.FirstOrDefault(x => x.Id == command.Id);

        if (brand is null)
        {
            throw NotFoundException.For<Brand>(
                command.Id,
                BrandErrorCode.NotFound);
        }

        brand.Name = command.Name;
        brand.LogoUrl = command.ImageUrl ?? "fake-logo.png";

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