using Gaming.Application.Common.Exceptions;
using Gaming.Application.Common.Interfaces;
using Gaming.Domain.Entities;
using MediatR;

namespace Gaming.Application.UseCases.ProductCase.Commands;

public class ProductUpdateCommand : IRequest<bool>
{
    public int ProductId { get; init; }
    public string ProductName { get; init; }
    public decimal Price { get; init; }
    public decimal Discount { get; init; }
    public string? Description { get; init; }
    public string? ProductImage { get; init; }
    public int? CategoryId { get; init; }

}
public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public ProductUpdateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FindAsync(new object[] { request.ProductId }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        entity.Price = request.Price;
        entity.Discount = request.Discount;
        entity.Description = request.Description;
        entity.ProductImage = request.ProductImage;
        entity.CategoryId = request.CategoryId;
        entity.ProductName = request.ProductName;


        if (await _context.SaveChangesAsync(cancellationToken) > 0)
            return true;
        return false;

    }
}
