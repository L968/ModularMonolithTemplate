using Microsoft.EntityFrameworkCore;

namespace ModularMonolithTemplate.Modules.Products.UnitTests.Application.Fixtures;

public sealed class ProductsDbContextFixture : IDisposable
{
    private readonly ProductsDbContext _dbContext;

    internal ProductsDbContext DbContext
    {
        get
        {
            ResetDatabase();
            return _dbContext;
        }
    }

    public ProductsDbContextFixture()
    {
        DbContextOptions<ProductsDbContext> options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ProductsDbContext(options);
    }

    public void ResetDatabase()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.ChangeTracker.Clear();
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}
