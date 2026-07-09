using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class ProductImage : Entity
{
    public string Url { get; internal set; } = default!;
    public string PreviewUrl { get; internal set; } = default!;
    public int ProductId { get; internal set; }
    public Product? Product { get; internal set; }


    public static ProductImage Create(
        string url,
        string previewUrl,
        Product product)
        => new()
        {
            Url = url,
            PreviewUrl = previewUrl,
            ProductId = product.Id,
            Product = product
        };

    public override bool Equals(object? obj) => obj is ProductImage at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}