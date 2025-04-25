using FS.Framework.Product.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using FS.Framework.Product.Domain.Entities;

namespace FS.FakeTwitter.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync() =>
               await _context.products.Where(u => !u.IsDeleted).ToListAsync();

        public async Task<ProductModel?> GetByIdAsync(Guid id) =>
            await _context.products.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        public async Task<ProductModel> AddAsync(ProductModel product)
        {
            var newproduct = await _context.products.AddAsync(product);
            return product;
        }

        public ProductModel Update(ProductModel product)
        {
            _context.products.Update(product);
            return product;
        }
        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.products.FirstOrDefaultAsync(u => u.Id == id);
            if (product is not null)
            {
                product.IsDeleted = true;
                _context.products.Update(product);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}