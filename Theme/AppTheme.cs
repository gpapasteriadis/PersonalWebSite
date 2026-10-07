using MudBlazor;

namespace PersonalWebSite.Theme;

/// <summary>
/// The single MudBlazor theme for the site. Identity: teal (light), blue-grey (dark), burlywood accent (Tertiary).
/// Font sizes use clamp() so typography scales with the viewport. Don't add per-breakpoint Typo switches in components.
/// </summary>
public static class AppTheme
{
    private const string Burlywood = "#DEB887";

    public static MudTheme Theme { get; } = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#00695C",
            PrimaryDarken = "#004D40",
            PrimaryLighten = "#26A69A",
            Secondary = "#00796B",
            Tertiary = Burlywood,
            TertiaryContrastText = "#3B2A14",
            Background = "#EEF6F5",
            BackgroundGray = "#E0F2F1",
            Surface = "#FFFFFF",
            AppbarBackground = "#00695C",
            AppbarText = "#FFFFFF",
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1C2B2A",
            TextPrimary = "#1C2B2A",
            TextSecondary = "#4A5C5A",
            ActionDefault = "#00695C",
            Divider = "#B2DFDB",
            LinesDefault = "#B2DFDB",
            Dark = "#00695C",
        },
        PaletteDark = new PaletteDark
        {
            Primary = "#B2DFDB",
            PrimaryContrastText = "#0F2E2B",
            PrimaryDarken = "#80CBC4",
            PrimaryLighten = "#E0F2F1",
            Secondary = "#80CBC4",
            Tertiary = Burlywood,
            TertiaryContrastText = "#3B2A14",
            Background = "#1A2327",
            BackgroundGray = "#202B30",
            Surface = "#263238",
            AppbarBackground = "#263238",
            AppbarText = "#ECEFF1",
            DrawerBackground = "#263238",
            DrawerText = "#ECEFF1",
            TextPrimary = "#ECEFF1",
            TextSecondary = "#B0BEC5",
            ActionDefault = "#CFD8DC",
            Divider = "#37474F",
            LinesDefault = "#37474F",
            Dark = "#263238",
        },
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Roboto", "Helvetica Neue", "Arial", "sans-serif"],
                LineHeight = "1.6",
            },
            H2 = new H2Typography { FontSize = "clamp(2.25rem, 1.6rem + 2.6vw, 3.5rem)", FontWeight = "500", LineHeight = "1.15", LetterSpacing = "-0.01em" },
            H3 = new H3Typography { FontSize = "clamp(1.875rem, 1.4rem + 1.9vw, 2.75rem)", FontWeight = "500", LineHeight = "1.2" },
            H4 = new H4Typography { FontSize = "clamp(1.5rem, 1.2rem + 1.2vw, 2.125rem)", FontWeight = "500", LineHeight = "1.25" },
            H5 = new H5Typography { FontSize = "clamp(1.2rem, 1.05rem + 0.6vw, 1.5rem)", FontWeight = "500", LineHeight = "1.35" },
            H6 = new H6Typography { FontSize = "clamp(1.05rem, 1rem + 0.3vw, 1.25rem)", FontWeight = "500", LineHeight = "1.4" },
            Body1 = new Body1Typography { FontSize = "clamp(0.95rem, 0.9rem + 0.25vw, 1.0625rem)", LineHeight = "1.7" },
            Body2 = new Body2Typography { FontSize = "0.9rem", LineHeight = "1.6" },
            Button = new ButtonTypography { TextTransform = "none", FontWeight = "500" },
        },
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "12px",
        },
    };
}
