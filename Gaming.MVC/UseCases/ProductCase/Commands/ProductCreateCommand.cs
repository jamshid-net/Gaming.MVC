using Gaming.MVC.Models;
using Gaming.MVC.Services;
using MediatR;

namespace Gaming.MVC.UseCases.ProductCase.Commands;

public class ProductCreateCommand:IRequest<bool>
{
    public string ProductName { get;init; }
    public decimal Price { get; init; }
    public decimal Discount { get; init; }
    public string? Description { get; init; }
    public string? ProductImage { get; init; }
    public int? CategoryId { get; init; } = 1;

}
public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public ProductCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        Product product = new Product()
        {
            Price = request.Price,
            Discount = request.Discount,
            Description = request.Description,
            ProductImage = request.ProductImage,
            CategoryId = request.CategoryId,
            ProductName = request.ProductName
        };
        await _context.Products.AddAsync(product,cancellationToken);
        if ((await _context.SaveChangesAsync(cancellationToken)) > 0)
            return true;
        return false;

    }
}
