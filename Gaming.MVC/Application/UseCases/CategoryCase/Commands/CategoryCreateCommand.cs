using Gaming.MVC.Application.Common.Interfaces;
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

    public Task<bool> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
