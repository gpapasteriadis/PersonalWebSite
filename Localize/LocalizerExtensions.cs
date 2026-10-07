using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace PersonalWebSite.Localize;

public static class LocalizerExtensions
{
    /// <summary>Renders a resx value that contains HTML (e.g. &lt;b&gt;, &lt;br/&gt;). Resx content is trusted, author-written markup.</summary>
    public static MarkupString Html(this IStringLocalizer localizer, string key) => new(localizer[key]);
}
