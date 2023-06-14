using AutoMapper;
using Gaming.Application.Common.Interfaces;
using Gaming.Application.Common.ModelDto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gaming.Application.UseCases.ProductCase.Queries;

public class ProductGetAllQuery:IRequest<List<ProductGetDto>>
{
}
public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, List<ProductGetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductGetAllQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProductGetDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
    {
        var entities =await _context.Products.AsNoTracking().ToListAsync(cancellationToken);
        var results = _mapper.Map<List<ProductGetDto>>(entities);
        return results;
    }
}
