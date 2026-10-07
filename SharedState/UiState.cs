using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PersonalWebSite.SharedState;

/// <summary>
/// App-wide UI state: culture, dark mode and the mobile drawer.
/// Preferences are persisted in localStorage through <c>window.site</c> (wwwroot/js/site.js).
/// Components that subscribe to <see cref="Changed"/> must unsubscribe in Dispose.
/// </summary>
public sealed class UiState(IJSRuntime js, NavigationManager navigation)
{
    public const string English = "en";
    public const string Greek = "el";

    public bool IsDarkMode { get; private set; }
    public bool IsDrawerOpen { get; private set; }

    public static bool IsGreek => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == Greek;

    public event Action? Changed;

    /// <summary>
    /// Runs once in Program.cs before the first render, so the right culture and theme are used from the start (no flash).
    /// </summary>
    public async Task InitializeAsync()
    {
        var prefs = await js.InvokeAsync<StoredPreferences>("site.prefs.get");

        var culture = new CultureInfo(prefs.Culture == Greek ? Greek : English);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        await js.InvokeVoidAsync("site.setCulture", culture.Name);

        IsDarkMode = prefs.DarkMode;
    }

    public async Task ToggleDarkModeAsync()
    {
        IsDarkMode = !IsDarkMode;
        NotifyChanged();
        await js.InvokeVoidAsync("site.setDarkMode", IsDarkMode);
    }

    /// <summary>Switches between English and Greek. Culture is applied at startup, so this reloads the page.</summary>
    public async Task ToggleCultureAsync()
    {
        await js.InvokeVoidAsync("site.setCulture", IsGreek ? English : Greek);
        // Drop the fragment: site.scrollToTop clears it outside Blazor, so NavigationManager.Uri can be stale.
        navigation.NavigateTo(navigation.Uri.Split('#')[0], forceLoad: true);
    }

    public void SetDrawerOpen(bool open)
    {
        if (IsDrawerOpen == open) return;
        IsDrawerOpen = open;
        NotifyChanged();
    }

    /// <summary>Scrolls to the top of the home page, navigating there first from other pages.</summary>
    public async Task GoHomeAsync()
    {
        SetDrawerOpen(false);
        var path = navigation.ToBaseRelativePath(navigation.Uri);
        if (path.Length == 0 || path.StartsWith('#'))
        {
            await js.InvokeVoidAsync("site.scrollToTop");
        }
        else
        {
            navigation.NavigateTo("/");
        }
    }

    private void NotifyChanged() => Changed?.Invoke();

    private sealed record StoredPreferences(string? Culture, bool DarkMode);
}
