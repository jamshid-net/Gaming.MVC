using AutoMapper;
using Gaming.Application.Common.ModelDto;
using Gaming.Domain.Entities;

namespace Gaming.Application.Common.Mappings;

public class OrderMapping:Profile
{
    public OrderMapping()
    {
        CreateMap<OrderGetDto,Order>().ReverseMap();
    }
}
