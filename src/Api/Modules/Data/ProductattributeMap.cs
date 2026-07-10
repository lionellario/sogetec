using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductAttributeMap : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        builder.ToTable("product_attribute");
        builder.HasKey(t => t.Id).HasName("PK_product_attribute");

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.NameFr).HasColumnName("name_fr").IsRequired();
        builder.Property(t => t.HeaderId).HasColumnName("header_id").IsRequired();
        builder.Property(t => t.IsVariant).HasColumnName("is_variant").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder
            .HasOne(c => c.Header)
            .WithMany(p => p.Attributes)
            .HasForeignKey(t => t.HeaderId)
            .HasConstraintName("FK_product_attribute_header")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.HeaderId).HasDatabaseName("IX_product_attribute_header");
    }
}
