namespace Api.Modules.Features.Products.Commands.Create;

public sealed class CreateProductHandler(SogetecDbContext db) : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    public async ValueTask<CreateProductResponse> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var brand = await db.Brands.

        var productItem = Product.Create(
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
            Details: command.Details is null ? null : new ProductDetails
            {
                ValeurInitialV0 = command.Details.ValeurInitialV0,
                ValeurResiduelleVT = command.Details.ValeurResiduelleVT,
                ValeurResiduelleUnitaire = command.Details.ValeurResiduelleUnitaire,
                AgeDays = command.Details.AgeDays
            }
        );

        db.Add(productItem);

        var attributeIds = command.Specifications.Select(x => x.ProductAttributeId).ToList();
        var attibutes = await db.ProductAttributes.Where(x => attributeIds.Contains(x.Id)).ToListAsync(cancellationToken);
        var attributeValues = command.Specifications.ToDictionary(a => a.ProductAttributeId, a => a.Value);

        foreach (var attr in attibutes)
        {
            var val = attributeValues[attr.Id];
            var variant = ProductVariant.Create(productItem, attr, val);
            db.Add(variant);
        }

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
            Details: productItem.Details is null ? null : new ProductDetailRecord(
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