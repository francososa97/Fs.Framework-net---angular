using global::FS.Framework.Product.Infrastructure.DataAccess;

namespace FS.Framework.Product.Infrastructure.UnitOfWork;


public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context,
        IProductRepository productRepository)
    {
        _context = context;
        Product = productRepository;
    }

    public IProductRepository Product { get; }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
