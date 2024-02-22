using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PersonalWebSite.SharedState
{
    public class StateContainer(IJSRuntime jsRuntime, NavigationManager navigationManager)
    {
        private readonly IJSRuntime _jsRuntime = jsRuntime;
        private readonly NavigationManager _navigationManager = navigationManager;

        public bool DarkModeIsOn { get; set; }
        public bool SideBarIsOpen { get; set; } = false;
        public event Action? DarkModeStateChanged;
        public event Action? SideBarStateChanged;


        public void NavigateHome()
        {
            _navigationManager.NavigateTo("/", forceLoad: true);
        }
        public async Task ScrollFunctionality()
        {
            await _jsRuntime.InvokeVoidAsync("scrollFunctionality");
        }

        public async Task CloseAppBar()
        {
            await _jsRuntime.InvokeVoidAsync("closeAppBar");
        }

        public void ChangeDarkModeState(bool? darkModeIsOn = null)
        {
            DarkModeIsOn = darkModeIsOn.HasValue ? (bool)darkModeIsOn : !DarkModeIsOn;
            NotifyDarkModeStateChanged();
        }

        public void ChangeSideBarState()
        {
            SideBarIsOpen = !SideBarIsOpen;
            NotifySideBarStateChanged();
        }

        private void NotifyDarkModeStateChanged() => DarkModeStateChanged?.Invoke();
        private void NotifySideBarStateChanged() => SideBarStateChanged?.Invoke();

    }
}
