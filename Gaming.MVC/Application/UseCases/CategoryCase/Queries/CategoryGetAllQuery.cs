using AutoMapper;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Application.Common.ModelDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gaming.MVC.Application.UseCases.CategoryCase.Queries;

public class CategoryGetAllQuery:IRequest<List<CategoryGetDto>>
{
}
public class CategoryGetAllQueryHandler : IRequestHandler<CategoryGetAllQuery, List<CategoryGetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CategoryGetAllQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryGetDto>> Handle(CategoryGetAllQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Categories.AsNoTracking().ToListAsync(cancellationToken);
        var results = _mapper.Map<List<CategoryGetDto>>(entities);
        return results;
    }
}
