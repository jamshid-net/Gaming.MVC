﻿using AutoMapper;
using Gaming.MVC.Application.Common.Exceptions;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Application.Common.ModelDto;
using Gaming.MVC.Domain.Models;
using MediatR;

namespace Gaming.MVC.Application.UseCases.ProductCase.Queries;

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
