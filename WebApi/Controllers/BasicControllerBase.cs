using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary.Basic;

namespace WebApi.Controllers;

[ApiController]
public abstract class BasicControllerBase : ControllerBase
{
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ??=
        HttpContext.RequestServices.GetRequiredService<ISender>();

    protected async Task<IActionResult> BasicAction<TModel, TResponse>(
        TModel model)
        where TModel : IRequest<RequestResponse<TResponse>>
    {
        return Ok(await Mediator.Send(model));
    }

    protected async Task<IActionResult> BasicGetAction<TModel, TResponse>(
        TModel model)
        where TModel : IRequest<RequestResponse<TResponse>>
    {
        return Ok(await Mediator.Send(model));

    }
}
