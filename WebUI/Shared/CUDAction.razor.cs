using Microsoft.JSInterop;

namespace WebUI.Shared;

public abstract partial class CUDAction<TDto> : ComponentBase
    where TDto : BaseDto, new()
{
    [Inject] IJSRuntime JS { get; set; } = null!;
    [Parameter] public RenderFragment<TDto>? ChildContent { get; set; }
    [Parameter] public EventCallback OnRefresh { get; set; }
    protected abstract string EntityName { get; }
    protected ValidationMessageStore? _messagesStore { get; set; }
    protected EditContext? EditContext { get; set; }
    protected TDto? UpdateModel { get; set; }
    protected bool flag;
    protected bool scrollAction = true;

    public async Task Create() => await SetEditContext(new TDto());
    public async Task Update(TDto dto)
    {
        UpdateModel = dto;
        await SetEditContext((TDto)dto.Clone());
    }

    private async Task SetEditContext(TDto dto)
    {
        if(scrollAction) await JS.InvokeVoidAsync("disableScroll");
        EditContext = new(dto);
        _messagesStore = new(EditContext);
        EditContext.OnValidationRequested += (s, e) => _messagesStore.Clear();
        EditContext.OnFieldChanged += (s, e)
            => _messagesStore.Clear(e.FieldIdentifier);
    }

    protected async Task Clear()
    {
        if(scrollAction) await JS.InvokeVoidAsync("enableScroll");
        EditContext = null;
        UpdateModel = null;
    }

    protected async Task Refresh()
    {
        await Clear();
        await OnRefresh.InvokeAsync();
    }
    protected abstract Task OnSubmit();
    protected abstract Task OnRemove();
}
