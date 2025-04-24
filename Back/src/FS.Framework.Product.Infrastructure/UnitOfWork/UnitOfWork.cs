using global::FS.Framework.Product.Infrastructure.DataAccess;

namespace FS.Framework.Product.Infrastructure.UnitOfWork;


public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context,
        IProductRepository userRepository)
    {
        _context = context;
        Users= userRepository;
    }

    public IProductRepository Users { get; }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
