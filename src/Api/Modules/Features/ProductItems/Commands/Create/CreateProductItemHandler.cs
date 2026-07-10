namespace Api.Modules.Features.ProductItems.Commands.Create;

public sealed class CreateProductItemHandler(SogetecDbContext db) : ICommandHandler<CreateProductItemCommand, CreateProductItemResponse>
{
    public async ValueTask<CreateProductItemResponse> Handle(CreateProductItemCommand command, CancellationToken cancellationToken)
    {
        var product = await db.Products.FirstOrDefaultAsync(c => c.Id == command.ProductId, cancellationToken);

        if (product is null)
        {
            throw NotFoundException.For<Product>(
                command.ProductId,
                ProductErrorCode.NotFound);
        }

        var productItem = ProductItem.Create(
            name: command.Name ?? product.Name,
            nameFr: command.NameFr ?? product.NameFr,
            code: command.Code,
            sku: command.Sku,
            product: product,
            price: command.Price,
            cost: command.Cost,
            initialStock: command.InitialStock,
            finalStock: command.FinalStock,
            quantityUnit: command.QuantityUnit,
            description: command.Description,
            Details: command.Details is null ? null : new ProductItemDetails
            {
                ValeurInitialV0 = command.Details.ValeurInitialV0,
                ValeurResiduelleVT = command.Details.ValeurResiduelleVT,
                ValeurResiduelleUnitaire = command.Details.ValeurResiduelleUnitaire,
                AgeDays = command.Details.AgeDays
            }
        );

        db.Add(productItem);

        await db.SaveChangesAsync(cancellationToken);

        return new(
            Id: productItem.Id,
            Name: productItem.Name,
            NameFr: productItem.NameFr,
            Slug: productItem.Slug,
            Code: productItem.Code,
            Sku: productItem.Sku,
            ProductId: productItem.ProductId,
            Price: productItem.Price,
            Cost: productItem.Cost,
            InitialStock: productItem.InitialStock,
            FinalStock: productItem.FinalStock,
            QuantityUnit: productItem.QuantityUnit,
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