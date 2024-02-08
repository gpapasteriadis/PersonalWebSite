namespace PersonalWebSite.SharedState
{
    public class StateContainer
    {
        public bool DarkModeIsOn { get; set; }
        public bool SideBarIsOpen { get; set; } = false;

        public event Action DarkModeStateChanged;
        public event Action SideBarStateChanged;

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
