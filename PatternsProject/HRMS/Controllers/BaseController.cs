using ApplicationCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }
}