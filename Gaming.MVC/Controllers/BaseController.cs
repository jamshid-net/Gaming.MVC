using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gaming.MVC.Controllers;
public class BaseController : Controller
{
    protected IMediator mediator
     => HttpContext.RequestServices.GetRequiredService<IMediator>();
}
