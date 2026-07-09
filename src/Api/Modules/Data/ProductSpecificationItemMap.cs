using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductSpecificationItemMap : IEntityTypeConfiguration<ProductSpecificationItem>
{
    public void Configure(EntityTypeBuilder<ProductSpecificationItem> builder)
    {
        builder.ToTable("product_specification_item");
        builder.HasKey(t => t.Id).HasName("PK_product_specification_item");

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Value).HasColumnName("value").IsRequired();
        builder.Property(t => t.ProductId).HasColumnName("product_id").IsRequired();
        builder.Property(t => t.ItemId).HasColumnName("item_id").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder
            .HasOne(c => c.Product)
            .WithMany(p => p.Specifications)
            .HasForeignKey(t => t.ProductId)
            .HasConstraintName("FK_product_specification_item_product")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.Item)
            .WithMany()
            .HasForeignKey(t => t.ItemId)
            .HasConstraintName("FK_product_specification_item_model_item")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.ProductId).HasDatabaseName("IX_product_specification_item_product");
        builder.HasIndex(t => t.ItemId).HasDatabaseName("IX_product_specification_item_item");
    }
}