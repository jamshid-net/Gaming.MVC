using AutoMapper;
using Gaming.MVC.Application.Common.Exceptions;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Application.Common.ModelDto;
using Gaming.MVC.Domain.Models;
using MediatR;

namespace Gaming.MVC.Application.UseCases.OrderCase.Queries;

public class OrderGetByIdQuery:IRequest<OrderGetDto>
{
    public int OrderId { get; init; }
}
public class OrderGetByIdQueryHandler : IRequestHandler<OrderGetByIdQuery, OrderGetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrderGetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<OrderGetDto> Handle(OrderGetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.FindAsync(new object[] { request.OrderId }, cancellationToken);
        if(entity is null) 
            throw new NotFoundException(nameof(Order),request.OrderId);

        var result = _mapper.Map<OrderGetDto>(entity);  

        return result;

    }
}
