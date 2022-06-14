using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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

    protected async Task<IActionResult> BasicAction<TModel>(
        TModel model, CacheList? cacheNames = null,
        CancellationToken ct = default(CancellationToken))
        where TModel : IBaseRequest
    {
        if (cacheNames != null) ClearCache(cacheNames);
            return Ok(await Mediator.Send(model, ct));
    }

    protected async Task<IActionResult> BasicGetAction<TModel>(
        TModel model, MemoryCacheNames? cacheName = null)
        where TModel : IBaseRequest
    {
        if (cacheName != null)
        {
            var cacheObject = Cache.Get(cacheName);
            if (cacheObject != null) return Ok(cacheObject);
        }

            var result = await Mediator.Send(model);
            if (cacheName != null) Cache.Set(
                cacheName, result, TimeSpan.FromDays(1));
            return Ok(result);
    }

    private void ClearCache(CacheList cacheNames)
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
