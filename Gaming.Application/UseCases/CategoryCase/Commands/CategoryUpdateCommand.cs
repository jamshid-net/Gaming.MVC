using Gaming.Application.Common.Exceptions;
using Gaming.Application.Common.Interfaces;
using Gaming.Domain.Entities;
using MediatR;

namespace Gaming.Application.UseCases.CategoryCase.Commands;

public class CategoryUpdateCommand : IRequest<bool>
{
    public int CategoryId { get; init; }
    public string CategoryName { get; init; }
    public string? CategoryImage { get; init; }
}
public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CategoryUpdateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);
        if (entity is null)
            throw new NotFoundException(nameof(Category), request.CategoryId);


        entity.CategoryImage = request.CategoryImage;
        entity.CategoryName = request.CategoryName;

        if ((await _context.SaveChangesAsync(cancellationToken)) > 0)
            return true;
        return false;
    }
}
