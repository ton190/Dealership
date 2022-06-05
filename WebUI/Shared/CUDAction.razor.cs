namespace WebUI.Shared;

public abstract partial class CUDAction<TDto> : ComponentBase
    where TDto : BaseDto, new()
{
    [Parameter] public RenderFragment<TDto>? ChildContent { get; set; }
    [Parameter] public EventCallback OnRefresh { get; set; }
    protected abstract string EntityName { get; }
    protected ValidationMessageStore? _messagesStore { get; set; }
    protected EditContext? EditContext { get; set; }
    protected TDto? UpdateModel { get; set; }

    public void Create() => SetEditContext(new TDto());
    public void Update(TDto dto)
    {
        UpdateModel = dto;
        SetEditContext((TDto)dto.Clone());
    }

    private void SetEditContext(TDto dto)
    {
        EditContext = new(dto);
        _messagesStore = new(EditContext);
        EditContext.OnValidationRequested += (s, e) => _messagesStore.Clear();
        EditContext.OnFieldChanged += (s, e)
            => _messagesStore.Clear(e.FieldIdentifier);
    }

    protected void Clear()
    {
        EditContext = null;
        UpdateModel = null;
    }

    protected async Task Refresh()
    {
        Clear();
        await OnRefresh.InvokeAsync();
    }
    protected abstract Task OnSubmit();
    protected abstract Task OnRemove();
}
