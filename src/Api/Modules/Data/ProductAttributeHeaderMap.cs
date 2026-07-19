using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductAttributeHeaderMap : IEntityTypeConfiguration<ProductAttributeHeader>
{
    public void Configure(EntityTypeBuilder<ProductAttributeHeader> builder)
    {
        builder.ToTable("product_attribute_header");
        builder.HasKey(t => t.Id).HasName("PK_product_attribute_header");

        builder.Property(t => t.Id).ValueGeneratedNever().HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.NameFr).HasColumnName("name_fr").IsRequired();
        builder.Property(t => t.SortOrder).HasColumnName("sort_order");
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
    }
}