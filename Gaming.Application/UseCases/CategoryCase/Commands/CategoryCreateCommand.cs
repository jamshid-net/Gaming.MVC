using Gaming.Application.Common.Interfaces;
using Gaming.Domain.Entities;
using MediatR;

namespace Gaming.Application.UseCases.CategoryCase.Commands;

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
