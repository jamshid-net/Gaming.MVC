using AutoMapper;
using Gaming.MVC.Application.Common.ModelDto;
using Gaming.MVC.Domain.Models;

namespace Gaming.MVC.Application.Common.Mappings;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<ProductGetDto, Product>().ReverseMap();

    }
}
