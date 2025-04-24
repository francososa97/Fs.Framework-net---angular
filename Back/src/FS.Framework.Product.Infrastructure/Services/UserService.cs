using FS.FakeTwitter.Application.Exceptions;
using FS.Framework.Product.Domain.Entities;

namespace FS.FakeTwitter.Application.Services;

public class UserService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductModel>> GetAllAsync() =>
        (await _unitOfWork.Users.GetAllAsync());

    public async Task<ProductModel?> GetByIdAsync(Guid id) =>
        (await _unitOfWork.Users.GetByIdAsync(id));

    public async Task<ProductModel> AddAsync(ProductModel user)
    {
        var NewUser = await _unitOfWork.Users.AddAsync(user);
        return NewUser;
    }

    public async Task<ProductModel> UpdateAsync(ProductModel user)
    {
        var existingUser = await _unitOfWork.Users.GetByIdAsync(user.Id);
        if (existingUser is null)
            throw new NotFoundException("Usuario no encontrado.");

        existingUser.Name = user.Name;
        existingUser.Price = user.Price;

        var updated = _unitOfWork.Users.Update(existingUser);
        await _unitOfWork.SaveChangesAsync();
        return updated;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        await _unitOfWork.Users.DeleteAsync(id);
        return await _unitOfWork.SaveChangesAsync();
    }
}