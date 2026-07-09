using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductImageMap : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("product_image");
        builder.HasKey(t => t.Id).HasName("PK_product_image");

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Url).HasColumnName("url").IsRequired();
        builder.Property(t => t.PreviewUrl).HasColumnName("preview_url").IsRequired();
        builder.Property(t => t.ProductId).HasColumnName("product_id").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder
            .HasOne(c => c.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(t => t.ProductId)
            .HasConstraintName("FK_product_image_product")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.ProductId).HasDatabaseName("IX_product_image_product");
    }
}