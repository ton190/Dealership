using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ModelLibrary.Basic;
using Shared;

namespace WebApi.Controllers;

[ApiController]
public abstract class BasicControllerBase : ControllerBase
{
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ??=
        HttpContext.RequestServices.GetRequiredService<ISender>();

    private IMemoryCache _memoryCache = null!;
    private IMemoryCache Cache => _memoryCache ??=
        HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

    protected async Task<IActionResult> BasicAction<TModel, TResponse>(
        TModel model, List<MemoryCacheNames>? cacheNames = null)
        where TModel : IRequest<RequestResponse<TResponse>>
    {
        if (cacheNames != null) ClearCache(cacheNames);
        try
        {
            return Ok(await Mediator.Send(model));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    protected async Task<object?> BasicGetAction<TModel, TResponse>(
        TModel model, MemoryCacheNames? cacheName = null)
        where TModel : IRequest<RequestResponse<TResponse>>
    {
        if (cacheName != null)
        {
            var cacheObject = Cache.Get(cacheName);
            if (cacheObject != null) return cacheObject;
        }

        try
        {
            var result = await Mediator.Send(model);
            if (cacheName != null) Cache.Set(
                cacheName, result, TimeSpan.FromDays(1));
            return result;
        }
        catch(Exception)
        {
            return default(TResponse);
        }

    }

    private void ClearCache(List<MemoryCacheNames> cacheNames)
    {
        if (cacheNames.Count() == 0)
        {
            var cacheNameList = Enum.GetValues(typeof(MemoryCacheNames));
            foreach (var cache in cacheNameList) Cache.Remove(cache);
        }
        else
        {
            foreach (var cacheName in cacheNames) Cache.Remove(cacheName);
        }
    }
}
