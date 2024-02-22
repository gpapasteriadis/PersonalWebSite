using Microsoft.Extensions.Localization;
using MudBlazor;

namespace PersonalWebSite.Localize
{
    public class Resource : MudLocalizer
    {
        private readonly IStringLocalizer<Resource> _localizer = null!;

        public Resource(IStringLocalizer<Resource> localizer)
        {
            _localizer = localizer;
        }
        public override LocalizedString this[string key] => _localizer[key];
    }
}
