namespace WebUI.Services;

public class AppSettings
{
    public bool PopupOpen { get; set; }

    public event Action? OnChange;

    public void Refresh() => OnChange?.Invoke();
}
