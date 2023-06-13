using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Domain.Models;
using MediatR;

namespace Gaming.MVC.Application.UseCases.CategoryCase.Commands;

public class CategoryCreateCommand:IRequest<bool>
{
    public string CategoryName { get; init; }
    public string? CategoryImage { get; init; }
}
public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CategoryCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        Category category = new Category
        {
            CategoryImage = request.CategoryImage,
            CategoryName = request.CategoryName

        };
        await _context.Categories.AddAsync(category,cancellationToken);
        if ((await _context.SaveChangesAsync(cancellationToken)) > 0)
            return true;
        return false;
    }
}
