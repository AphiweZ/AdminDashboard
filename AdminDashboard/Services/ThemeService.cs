namespace AdminDashboard.Services;

/// <summary>
/// Tracks light/dark mode for the current circuit and notifies subscribers
/// (e.g. the layout) when it changes so they can re-render.
/// </summary>
public class ThemeService
{
    public bool IsDarkMode { get; private set; }

    public event Action? OnChange;

    public void SetDarkMode(bool isDark)
    {
        if (IsDarkMode == isDark) return;
        IsDarkMode = isDark;
        OnChange?.Invoke();
    }

    public void Toggle() => SetDarkMode(!IsDarkMode);
}
