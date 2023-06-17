using Gaming.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gaming.MVC.Controllers;
public class BaseController : Controller
{
    protected IMediator mediator
     => HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected IApplicationDbContext _context
        => HttpContext.RequestServices.GetRequiredService<IApplicationDbContext>();

    protected IWebHostEnvironment _webHostEnvironment 
        => HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

}
