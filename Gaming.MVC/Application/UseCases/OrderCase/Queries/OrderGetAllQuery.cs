using AutoMapper;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Application.Common.ModelDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gaming.MVC.Application.UseCases.OrderCase.Queries;

public class OrderGetAllQuery:IRequest<List<OrderGetDto>>
{

}
public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQuery, List<OrderGetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrderGetAllQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderGetDto>> Handle(OrderGetAllQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Orders.AsNoTracking().ToListAsync(cancellationToken);
        var results = _mapper.Map<List<OrderGetDto>>(entities);
        return results;

    }
}
