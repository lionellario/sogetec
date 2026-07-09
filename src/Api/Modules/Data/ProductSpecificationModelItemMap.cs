using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductSpecificationModelItemMap : IEntityTypeConfiguration<ProductSpecificationModelItem>
{
    public void Configure(EntityTypeBuilder<ProductSpecificationModelItem> builder)
    {
        builder.ToTable("product_specification_model_item");
        builder.HasKey(t => t.Id).HasName("PK_product_specification_model_item");

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.NameFr).HasColumnName("name_fr").IsRequired();
        builder.Property(t => t.HeaderId).HasColumnName("header_id").IsRequired();
        builder.Property(t => t.IsVariant).HasColumnName("is_variant").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder
            .HasOne(c => c.Header)
            .WithMany(p => p.Items)
            .HasForeignKey(t => t.HeaderId)
            .HasConstraintName("FK_product_specification_model_item_product_specification_model")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.HeaderId).HasDatabaseName("IX_product_specification_model_item_header");
    }
}
