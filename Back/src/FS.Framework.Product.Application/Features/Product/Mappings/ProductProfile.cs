using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using FS.Framework.Product.Application.Features.Users.Commands.CreateUserCommand;
using FS.Framework.Product.Application.Features.Users.Commands.UpdateUserCommand;
using FS.Framework.Product.Domain.Entities;
namespace FS.Framework.Product.Application.Features.Product.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductModel, ProductDto>().ReverseMap();
        CreateMap<CreateProductCommand, ProductModel>();
        CreateMap<UpdateProductCommand, ProductModel>();
    }
}