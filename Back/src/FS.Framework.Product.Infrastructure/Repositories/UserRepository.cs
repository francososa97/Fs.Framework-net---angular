using FS.Framework.Product.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using FS.Framework.Product.Domain.Entities;

namespace FS.FakeTwitter.Infrastructure.Repositories
{
    public class UserRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync() =>
               await _context.products.Where(u => !u.IsDeleted).ToListAsync();

        public async Task<ProductModel?> GetByIdAsync(Guid id) =>
            await _context.products.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        public async Task<ProductModel> AddAsync(ProductModel user)
        {
            var newUser = await _context.products.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public ProductModel Update(ProductModel user)
        {
            _context.products.Update(user);
            return user;
        }
        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.products.FirstOrDefaultAsync(u => u.Id == id);
            if (user is not null)
            {
                user.IsDeleted = true;
                _context.products.Update(user);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}