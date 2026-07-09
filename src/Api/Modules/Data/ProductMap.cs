using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");
        builder.HasKey(t => t.Id).HasName("PK_product");

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.NameFr).HasColumnName("name_fr").IsRequired();
        builder.Property(t => t.Slug).HasColumnName("slug").IsRequired();
        builder.Property(t => t.Description).HasColumnName("description");
        builder.Property(t => t.IsActive).HasColumnName("is_active").IsRequired();
        builder.Property(t => t.BrandId).HasColumnName("brand_id").IsRequired();
        builder.Property(t => t.CategoryId).HasColumnName("category_id").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder
            .HasOne(c => c.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .HasConstraintName("FK_product_category")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(c => c.Brand)
            .WithMany()
            .HasForeignKey(t => t.BrandId)
            .HasConstraintName("FK_product_brand")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.CategoryId).HasDatabaseName("IX_product_category");
        builder.HasIndex(t => t.BrandId).HasDatabaseName("IX_product_brand");
    }
}