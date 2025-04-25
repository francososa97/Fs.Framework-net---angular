using AutoMapper;
using FS.Framework.Product.Application.DTOs;
using FS.Framework.Product.Application.Features.Product.Commands.CreateProduct;
using FS.Framework.Product.Application.Features.Product.Commands.UpdateProduct;
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