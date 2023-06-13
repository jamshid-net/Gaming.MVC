using Gaming.MVC.Application.Common.Exceptions;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gaming.MVC.Application.UseCases.OrderCase.Commands;

public class OrderUpdateCommand : IRequest<bool>
{
    public int OrderId { get; init; }
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}
public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public OrderUpdateCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
    {

        var OrderEntity =await _context.Orders
            .FindAsync(
            new object[] {request.OrderId},
            cancellationToken);

      

        var ProductEntity = await _context.Products
            .FindAsync(
            new object[] { request.ProductId },
            cancellationToken);

        var CurrentUserEntity = await _context.Users
            .SingleOrDefaultAsync
            (x => x.UserId == _currentUser.UserId
            && x.UserName == _currentUser.UserName,
            cancellationToken);

        if (ProductEntity is null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        if (OrderEntity is null)
            throw new NotFoundException(nameof(Order), request.OrderId);

        if (CurrentUserEntity is null)
            throw new NotFoundException(nameof(User));

            OrderEntity.ProductId = ProductEntity.ProductId;
            OrderEntity.Quantity = request.Quantity;
            OrderEntity.UserId = CurrentUserEntity.UserId;
            OrderEntity.OrderDate = DateTime.Now;
        
       
        if ((await _context.SaveChangesAsync(cancellationToken)) > 0)
            return true;
        return false;


    }
}