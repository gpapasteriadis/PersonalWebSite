namespace PersonalWebSite.Models;

// Records describing the portfolio content. Every *Key property is a resx key
// (Resources/Localize.Resource*.resx); every *LinkKey is a configuration path
// in wwwroot/appsettings.json.

/// <summary>An entry in the app bar / drawer navigation that scrolls to a home-page section.</summary>
/// <param name="Page">Page that hosts the section; the home page by default.</param>
public sealed record NavItem(string SectionId, string LabelKey, string Page = "/")
{
    public string Href => $"{Page}#{SectionId}";
}

/// <summary>A position in the Experience section.</summary>
/// <param name="Company">Company name shown after the role, or a resx key when <paramref name="IsLocalized"/>.</param>
/// <param name="ShortName">Tab label on mobile, or a resx key when <paramref name="IsLocalized"/>.</param>
public sealed record Job(
    string Company,
    string ShortName,
    string RoleKey,
    string DateKey,
    string DescriptionKey,
    string LinkKey,
    bool IsLocalized = false);

/// <summary>A card in the Projects section.</summary>
public sealed record Project(
    string LabelKey,
    string TitleKey,
    string DescriptionKey,
    IReadOnlyList<string> Tech);

/// <summary>A chip in the Skills section; clicking it shows the description in a snackbar.</summary>
/// <param name="Animation">animate.css keyframe name played when the section scrolls into view.</param>
public sealed record Skill(
    string Name,
    string IconSvg,
    string DescriptionKey,
    string AcquiredKey,
    string Animation);

/// <summary>A social network icon link in the Contact section.</summary>
public sealed record SocialLink(string Name, string Icon, string LinkKey);

// --- Project case-study pages (e.g. /Fieldmatics) -------------------------------------------

/// <summary>An icon card with a title and a short text (value propositions, principles).</summary>
/// <param name="IsPlanned">Shows a "coming next" badge for roadmap features.</param>
public sealed record Feature(string Icon, string TitleKey, string TextKey, bool IsPlanned = false);

/// <summary>The phase a journey step belongs to; drives the label and accent color.</summary>
public enum JourneyZone
{
    Setup,
    Daily,
    Next,
}

/// <summary>One step of the illustrated "how it works" journey.</summary>
public sealed record JourneyStep(string Icon, JourneyZone Zone, string TitleKey, string TextKey);

/// <summary>A Mermaid diagram; <paramref name="SourceKey"/> holds the localized Mermaid source.</summary>
public sealed record Diagram(string Id, string TitleKey, string TextKey, string SourceKey);
