namespace Api.Modules.Data.Context;

public class SogetecDbContext(DbContextOptions<SogetecDbContext> Options) : DbContext(Options)
{
    public IQueryable<Brand> Brands => Set<Brand>();
    public IQueryable<Category> Categories => Set<Category>();
    public IQueryable<CategoryGroup> CategoryGroups => Set<CategoryGroup>();
    public IQueryable<Product> Products => Set<Product>();
    public IQueryable<ProductItem> ProductItems => Set<ProductItem>();
    public IQueryable<ProductImage> ProductImages => Set<ProductImage>();
    public IQueryable<ProductSpecificationModel> ProductSpecificationModels => Set<ProductSpecificationModel>();
    public IQueryable<ProductSpecificationModelItem> ProductSpecificationModelItems => Set<ProductSpecificationModelItem>();
    public IQueryable<ProductSpecificationItem> ProductSpecificationItems => Set<ProductSpecificationItem>();


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
