using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class ProductItemMap : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder.ToTable("product_item");
        builder.HasKey(t => t.Id).HasName("PK_product_item");

        builder.Property(t => t.Id).ValueGeneratedNever().HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.NameFr).HasColumnName("name_fr").IsRequired();
        builder.Property(t => t.Slug).HasColumnName("slug").IsRequired();
        builder.Property(t => t.Code).HasColumnName("code").IsRequired();
        builder.Property(t => t.Sku).HasColumnName("sku").IsRequired();
        builder.Property(t => t.Description).HasColumnName("description");
        builder.Property(t => t.IsActive).HasColumnName("is_active").IsRequired();
        builder.Property(t => t.ProductId).HasColumnName("product_id").IsRequired();
        builder.Property(t => t.PriceAdjustment).HasColumnName("price_adjustment").IsRequired();
        builder.Property(t => t.CostAdjustment).HasColumnName("cost_adjustment").IsRequired();
        builder.Property(t => t.InitialStock).HasColumnName("initial_stock").IsRequired();
        builder.Property(t => t.FinalStock).HasColumnName("final_stock").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder
            .HasOne(c => c.Product)
            .WithMany(p => p.Items)
            .HasForeignKey(t => t.ProductId)
            .HasConstraintName("FK_product_item_product")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .OwnsOne(c => c.Details, b =>
            {
                b.ToJson("details");
            });

        builder.HasIndex(t => t.ProductId).HasDatabaseName("IX_product_item_product");
    }
}