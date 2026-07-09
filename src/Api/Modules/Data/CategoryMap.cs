using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category");
        builder.HasKey(t => t.Id).HasName("PK_category");

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.NameFr).HasColumnName("name_fr").IsRequired();
        builder.Property(t => t.Slug).HasColumnName("slug").IsRequired();
        builder.Property(t => t.ParentId).HasColumnName("parent_id");
        builder.Property(t => t.GroupId).HasColumnName("group_id").IsRequired();
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

        builder
            .HasOne(c => c.Group)
            .WithMany(p => p.Categories)
            .HasForeignKey(t => t.GroupId)
            .HasConstraintName("FK_category_group")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.GroupId).HasDatabaseName("IX_category_group");
        builder.HasIndex(t => t.ParentId).HasDatabaseName("IX_category_parent");
        builder.HasIndex(t => t.Slug).HasDatabaseName("IX_category_slug").IsUnique();
    }
}