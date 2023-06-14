using Gaming.Application.Common.Exceptions;
using Gaming.Application.Common.Interfaces;
using Gaming.Domain.Entities;
using MediatR;

namespace Gaming.Application.UseCases.CategoryCase.Commands;

public class CategoryDeleteCommand : IRequest<bool>
{
    public int CategoryId { get; init; }
}
public class CategoryDeleteCommandHandler : IRequestHandler<CategoryDeleteCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CategoryDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Category), request.CategoryId);
        _context.Categories.Remove(entity);
        if ((await _context.SaveChangesAsync(cancellationToken)) > 0)
            return true;
        return false;
    }
}
