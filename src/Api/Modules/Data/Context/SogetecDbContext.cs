namespace Api.Modules.Data.Context;

public class SogetecDbContext(DbContextOptions<SogetecDbContext> Options) : DbContext(Options)
{
    public IQueryable<Brand> Brands => Set<Brand>();
    public IQueryable<Category> Categories => Set<Category>();
    public IQueryable<CategoryGroup> CategoryGroups => Set<CategoryGroup>();
    public IQueryable<Product> Products => Set<Product>();
    public IQueryable<ProductItem> ProductItems => Set<ProductItem>();
    public IQueryable<ProductImage> ProductImages => Set<ProductImage>();
    public IQueryable<ProductAttributeHeader> ProductAttributeHeaders => Set<ProductAttributeHeader>();
    public IQueryable<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public IQueryable<ProductVariant> ProductVariants => Set<ProductVariant>();


    public override int SaveChanges()
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SogetecDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
