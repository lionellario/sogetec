using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductItemVariantMap : IEntityTypeConfiguration<ProductItemVariant>
{
    public void Configure(EntityTypeBuilder<ProductItemVariant> builder)
    {
        builder.ToTable("product_item_variant");
        builder.HasKey(t => t.Id).HasName("PK_product_item_variant");

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.ItemId).HasColumnName("item_id").IsRequired();
        builder.Property(t => t.VariantId).HasColumnName("variant_id").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder
            .HasOne(c => c.Item)
            .WithMany(p => p.Variants)
            .HasForeignKey(t => t.ItemId)
            .HasConstraintName("FK_product_item_variant_product_item")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.Variant)
            .WithMany()
            .HasForeignKey(t => t.VariantId)
            .HasConstraintName("FK_product_item_variant_product_variant")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.ItemId).HasDatabaseName("IX_product_item_variant_item");
        builder.HasIndex(t => t.VariantId).HasDatabaseName("IX_product_item_variant_variant");
    }
}