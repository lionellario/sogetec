using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Modules.Data;

public sealed class BrandMap : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("brand");
        builder.HasKey(t => t.Id).HasName("PK_brand");

        builder.Property(t => t.Id).HasColumnName("id");
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.LogoUrl).HasColumnName("logo_url").IsRequired();
        builder.Property(t => t.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
        builder.Property(t => t.LastModifiedOn).HasColumnName("last_modified_on").HasDefaultValueSql("CURRENT_TIMESTAMP").IsRequired();
    }
}