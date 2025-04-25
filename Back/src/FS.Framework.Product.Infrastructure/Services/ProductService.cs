using FS.FakeTwitter.Application.Exceptions;
using FS.Framework.Product.Application.Interfaces.Cache;
using FS.Framework.Product.Domain.Entities;

namespace FS.FakeTwitter.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheHelper _cacheHelper;

    public ProductService(IUnitOfWork unitOfWork, ICacheHelper cacheHelper)
    {
        _unitOfWork = unitOfWork;
        _cacheHelper = cacheHelper;
    }


    public async Task<IEnumerable<ProductModel>> GetAllAsync()
    {
        return await _cacheHelper.GetOrSetProductsAsync(async () =>
        {
            try
            {
                return await _unitOfWork.Product.GetAllAsync();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ProductModel>();
            }
        });
    }

    public async Task<ProductModel> GetByIdAsync(Guid id)
        => await _unitOfWork.Product.GetByIdAsync(id);

    public async Task<ProductModel> AddAsync(ProductModel product)
     => await _unitOfWork.Product.AddAsync(product);
         

    public async Task<ProductModel> UpdateAsync(ProductModel product)
    {
        var existingproduct = await _unitOfWork.Product.GetByIdAsync(product.Id);
        if (existingproduct is null)
            throw new NotFoundException("Producto no encontrado.");

        existingproduct.Name = product.Name;
        existingproduct.Price = product.Price;
        existingproduct.Stock = product.Stock;

        var updated = _unitOfWork.Product.Update(existingproduct);
        await _unitOfWork.SaveChangesAsync();
        return updated;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        await _unitOfWork.Product.DeleteAsync(id);
        return await _unitOfWork.SaveChangesAsync();
    }
}