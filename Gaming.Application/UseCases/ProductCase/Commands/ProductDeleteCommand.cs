using Gaming.Application.Common.Exceptions;
using Gaming.Application.Common.Interfaces;
using Gaming.Domain.Entities;
using MediatR;

namespace Gaming.Application.UseCases.ProductCase.Commands;

public class ProductDeleteCommand : IRequest<bool>
{
    public int ProductId { get; init; }
}
public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public ProductDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FindAsync(new object[] { request.ProductId }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Product), request.ProductId);
        _context.Products.Remove(entity);
        if (await _context.SaveChangesAsync(cancellationToken) > 0)
            return true;
        return false;

    }
}
