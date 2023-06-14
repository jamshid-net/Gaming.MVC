using AutoMapper;
using Gaming.Application.Common.ModelDto;
using Gaming.Domain.Entities;

namespace Gaming.Application.Common.Mappings;

public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<ProductGetDto, Product>().ReverseMap();

    }
}
