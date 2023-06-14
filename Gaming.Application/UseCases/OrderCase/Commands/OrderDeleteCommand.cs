using Gaming.Application.Common.Exceptions;
using Gaming.Application.Common.Interfaces;
using Gaming.Domain.Entities;
using MediatR;

namespace Gaming.Application.UseCases.OrderCase.Commands;

public class OrderDeleteCommand:IRequest<bool>
{
    public int OrderId { get; init; }
}
public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public OrderDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity =await _context.Orders.FindAsync(new object[] {request.OrderId}, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Order), request.OrderId);

         _context.Orders.Remove(entity);
       if((await _context.SaveChangesAsync(cancellationToken))>0)
            return true;
       return false;
    }
}
