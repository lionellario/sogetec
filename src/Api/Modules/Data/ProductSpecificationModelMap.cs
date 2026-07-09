using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductSpecificationModelMap : IEntityTypeConfiguration<ProductSpecificationModel>
{
    public void Configure(EntityTypeBuilder<ProductSpecificationModel> builder)
    {
        builder.ToTable("product_specification_model");
        builder.HasKey(t => t.Id).HasName("PK_product_specification_model");

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.NameFr).HasColumnName("name_fr").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
    }
}