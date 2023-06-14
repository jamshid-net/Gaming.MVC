using AutoMapper;
using Gaming.Application.Common.Exceptions;
using Gaming.Application.Common.Interfaces;
using Gaming.Application.Common.ModelDto;
using Gaming.Domain.Entities;
using MediatR;

namespace Gaming.Application.UseCases.ProductCase.Queries;

public class ProductGetByIdQuery:IRequest<ProductGetDto>
{
    public int ProductId { get; init; }
}
public class ProductGetByIdQueryHandler : IRequestHandler<ProductGetByIdQuery, ProductGetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ProductGetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<ProductGetDto> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FindAsync(new object[] { request.ProductId },cancellationToken);
        if (entity is null) 
            throw new NotFoundException(nameof(Product),request.ProductId);

        var result = _mapper.Map<ProductGetDto>(entity);

        return result;

    }
}
