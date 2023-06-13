using AutoMapper;
using Gaming.MVC.Application.Common.ModelDto;
using Gaming.MVC.Domain.Models;

namespace Gaming.MVC.Application.Common.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<CategoryGetDto,Category>().ReverseMap();
    }
}
