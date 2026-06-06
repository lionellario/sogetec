using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class AccountingCategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category");
        builder.HasKey(t => t.Id).HasName("PK_category");

        builder.Property(t => t.Id).ValueGeneratedNever().HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.Slug).HasColumnName("slug").IsRequired();
        builder.Property(t => t.ParentId).HasColumnName("parent_id");
        builder.Property(t => t.Description).HasColumnName("description");
        builder.Property(t => t.ImageUrl).HasColumnName("image_url");
        builder.Property(t => t.IsActive).HasColumnName("is_active");
        builder.Property(t => t.SortOrder).HasColumnName("sort_order");
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();

        builder
            .HasOne(c => c.Parent)
            .WithMany(p => p.SubCategories)
            .HasForeignKey(t => t.ParentId)
            .HasConstraintName("FK_category_category")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(t => t.ParentId).HasDatabaseName("IX_category_parent");
        builder.HasIndex(t => t.Slug).HasDatabaseName("IX_category_slug").IsUnique();
    }
}