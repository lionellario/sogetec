using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductVariantMap : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable("product_variant");
        builder.HasKey(t => t.Id).HasName("PK_product_variant");

        builder.Property(t => t.Id).ValueGeneratedNever().HasColumnName("id");
        builder.Property(t => t.ItemId).HasColumnName("item_id").IsRequired();
        builder.Property(t => t.AttributeId).HasColumnName("attribute_id").IsRequired();
        builder.Property(t => t.Value).HasColumnName("value").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder
            .HasOne(c => c.Item)
            .WithMany(p => p.Variants)
            .HasForeignKey(t => t.ItemId)
            .HasConstraintName("FK_product_variant_product_item")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.Attribute)
            .WithMany()
            .HasForeignKey(t => t.AttributeId)
            .HasConstraintName("FK_product_variant_product_attribute")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.ItemId).HasDatabaseName("IX_product_variant_item");
        builder.HasIndex(t => t.AttributeId).HasDatabaseName("IX_product_variant_attribute");
    }
}