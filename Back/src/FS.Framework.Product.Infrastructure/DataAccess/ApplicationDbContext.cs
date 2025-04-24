using FS.Framework.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FS.Framework.Product.Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProductModel> products { get; set; }
}
