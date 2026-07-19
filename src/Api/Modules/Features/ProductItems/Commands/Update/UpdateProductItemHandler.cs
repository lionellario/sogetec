using Sogetec.Chassis.Extensions;

namespace Api.Modules.Features.ProductItems.Commands.Update;

public sealed class GetProductItemByIdHandler(SogetecDbContext db) : ICommandHandler<UpdateProductItemCommand, UpdateProductItemResponse>
{
    public async ValueTask<UpdateProductItemResponse> Handle(UpdateProductItemCommand command, CancellationToken cancellationToken)
    {
        var productItem = await db.ProductItems.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (productItem is null)
        {
            throw NotFoundException.For<ProductItem>(
                command.Id,
                ProductErrorCode.ProductItemNotFound);
        }

        productItem.Name = command.Name;
        productItem.NameFr = command.NameFr;
        productItem.Slug = command.Name.Slugify();
        productItem.Code = command.Code;
        productItem.Sku = command.Sku;
        productItem.PriceAdjustment = command.PriceAdjustment;
        productItem.CostAdjustment = command.CostAdjustment;
        productItem.Description = command.Description;
        productItem.InitialStock = command.InitialStock;
        productItem.FinalStock = command.FinalStock;
        productItem.IsActive = command.IsActive;
        productItem.Details = command.Details is null ? productItem.Details : new ProductItemDetails
        {
            ValeurInitialV0 = command.Details.ValeurInitialV0,
            ValeurResiduelleVT = command.Details.ValeurResiduelleVT,
            ValeurResiduelleUnitaire = command.Details.ValeurResiduelleUnitaire,
            AgeDays = command.Details.AgeDays
        };

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: productItem.Id,
            Name: productItem.Name,
            NameFr: productItem.NameFr,
            Slug: productItem.Slug,
            Code: productItem.Code,
            Sku: productItem.Sku,
            ProductId: productItem.ProductId,
            PriceAdjustment: productItem.PriceAdjustment,
            CostAdjustment: productItem.CostAdjustment,
            InitialStock: productItem.InitialStock,
            FinalStock: productItem.FinalStock,
            Description: productItem.Description,
            IsActive: productItem.IsActive,
            Details: productItem.Details is null ? null : new ProductItemDetailRecord(
                ValeurInitialV0: productItem.Details.ValeurInitialV0,
                ValeurResiduelleVT: productItem.Details.ValeurResiduelleVT,
                ValeurResiduelleUnitaire: productItem.Details.ValeurResiduelleUnitaire,
                AgeDays: productItem.Details.AgeDays
            ),
            CreatedAt: productItem.CreatedOn,
            LastModifiedAt: productItem.LastModifiedOn
        );
    }
}