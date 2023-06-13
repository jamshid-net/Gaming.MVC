using Gaming.MVC.Application.Common.Exceptions;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gaming.MVC.Application.UseCases.OrderCase.Commands;

public class OrderCreateCommand : IRequest<bool>
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}
public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public OrderCreateCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
    {
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

        if (CurrentUserEntity is null)
            throw new NotFoundException(nameof(User));

        Order order = new Order
        {
            ProductId = ProductEntity.ProductId,
            Quantity = request.Quantity,
            UserId = CurrentUserEntity.UserId,
            OrderDate = DateTime.Now
        };

        await _context.Orders.AddAsync(order, cancellationToken);
        if ((await _context.SaveChangesAsync(cancellationToken)) > 0)
            return true;
        return false;


    }
}
