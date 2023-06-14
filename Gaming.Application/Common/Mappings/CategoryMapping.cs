using AutoMapper;
using Gaming.Application.Common.ModelDto;
using Gaming.Domain.Entities;


namespace Gaming.Application.Common.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<CategoryGetDto,Category>().ReverseMap();
    }
}
