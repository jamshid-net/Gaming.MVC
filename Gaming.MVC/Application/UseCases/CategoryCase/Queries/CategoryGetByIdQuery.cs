using AutoMapper;
using Gaming.MVC.Application.Common.Exceptions;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Application.Common.ModelDto;
using Gaming.MVC.Domain.Models;
using MediatR;

namespace Gaming.MVC.Application.UseCases.CategoryCase.Queries;

public class CategoryGetByIdQuery:IRequest<CategoryGetDto>
{
    public int CategoryId { get; init; }
}
public class CategoryGetByIdQueryHandler : IRequestHandler<CategoryGetByIdQuery, CategoryGetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CategoryGetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CategoryGetDto> Handle(CategoryGetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.FindAsync(new object[] { request.CategoryId },cancellationToken);
        if(entity is null)
            throw new NotFoundException(nameof(Category),request.CategoryId);

        var result = _mapper.Map<CategoryGetDto>(entity);
        return result;

    }
}
