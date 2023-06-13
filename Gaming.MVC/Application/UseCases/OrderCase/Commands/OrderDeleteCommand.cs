using Gaming.MVC.Application.Common.Exceptions;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Domain.Models;
using MediatR;

namespace Gaming.MVC.Application.UseCases.OrderCase.Commands;

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
