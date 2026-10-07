using MudBlazor;
using PersonalWebSite.Models;

namespace PersonalWebSite.Data;

/// <summary>
/// Content of the /Fieldmatics case-study page. Fieldmatics is a private, proprietary repository:
/// show business and architecture, never code, configuration, topic names or internal decision IDs.
/// Source material: the Fieldmatics README, ARCHITECTURE.md and docs/architecture/ in that repository.
/// </summary>
public static class FieldmaticsContent
{
    public const string PagePath = "/Fieldmatics";

    /// <summary>In-page navigation; section ids double as scrollspy targets.</summary>
    public static IReadOnlyList<NavItem> Sections { get; } =
    [
        new("fm-business", "FmBusinessTitle", PagePath),
        new("fm-how", "FmJourneyTitle", PagePath),
        new("fm-design", "FmDesignTitle", PagePath),
        new("fm-decisions", "FmDecisionsTitle", PagePath),
        new("fm-stack", "FmStackTitle", PagePath),
    ];

    public static IReadOnlyList<string> EnvironmentKeys { get; } =
        ["FmEnvGreenhouses", "FmEnvGardens", "FmEnvFields", "FmEnvOliveGroves"];

    public static IReadOnlyList<Feature> Highlights { get; } =
    [
        new(Icons.Material.Filled.Place, "FmHlLocationTitle", "FmHlLocationText"),
        new(Icons.Material.Filled.Hub, "FmHlAgnosticTitle", "FmHlAgnosticText"),
        new(Icons.Material.Filled.Security, "FmHlSaasTitle", "FmHlSaasText"),
        new(Icons.Material.Filled.AutoMode, "FmHlAutomationTitle", "FmHlAutomationText"),
    ];

    public static IReadOnlyList<JourneyStep> Journey { get; } =
    [
        new(Icons.Material.Filled.Sensors, JourneyZone.Site, "FmStep1Title", "FmStep1Text", "FmStep1Tech"),
        new(Icons.Material.Filled.SettingsInputAntenna, JourneyZone.Site, "FmStep2Title", "FmStep2Text", "FmStep2Tech"),
        new(Icons.Material.Filled.MarkEmailUnread, JourneyZone.Inside, "FmStep3Title", "FmStep3Text", "FmStep3Tech"),
        new(Icons.Material.Filled.Queue, JourneyZone.Inside, "FmStep4Title", "FmStep4Text", "FmStep4Tech"),
        new(Icons.Material.Filled.FactCheck, JourneyZone.Inside, "FmStep5Title", "FmStep5Text", "FmStep5Tech"),
        new(Icons.Material.Filled.Storage, JourneyZone.Inside, "FmStep6Title", "FmStep6Text", "FmStep6Tech"),
        new(Icons.Material.Filled.Api, JourneyZone.Inside, "FmStep7Title", "FmStep7Text", "FmStep7Tech"),
        new(Icons.Material.Filled.Insights, JourneyZone.User, "FmStep8Title", "FmStep8Text", "FmStep8Tech"),
    ];

    public static string ZoneKey(JourneyZone zone) => zone switch
    {
        JourneyZone.Site => "FmZoneSite",
        JourneyZone.Inside => "FmZoneInside",
        _ => "FmZoneGrower",
    };

    public static IReadOnlyList<Diagram> Diagrams { get; } =
    [
        new("fm-diagram-context", "FmDiagContextTitle", "FmDiagContextText", "FmDiagContextSource"),
        new("fm-diagram-processes", "FmDiagProcessesTitle", "FmDiagProcessesText", "FmDiagProcessesSource"),
        new("fm-diagram-flow", "FmDiagFlowTitle", "FmDiagFlowText", "FmDiagFlowSource"),
        new("fm-diagram-domain", "FmDiagDomainTitle", "FmDiagDomainText", "FmDiagDomainSource"),
    ];

    public static IReadOnlyList<Feature> Decisions { get; } =
    [
        new(Icons.Material.Filled.DeveloperBoardOff, "FmDec1Title", "FmDec1Text"),
        new(Icons.Material.Filled.SwapHoriz, "FmDec2Title", "FmDec2Text"),
        new(Icons.Material.Filled.TaskAlt, "FmDec3Title", "FmDec3Text"),
        new(Icons.Material.Filled.VerifiedUser, "FmDec4Title", "FmDec4Text"),
        new(Icons.Material.Filled.History, "FmDec5Title", "FmDec5Text"),
        new(Icons.Material.Filled.Rule, "FmDec6Title", "FmDec6Text"),
    ];

    public static IReadOnlyList<TechGroup> TechStack { get; } =
    [
        new("FmStackBackend", [".NET 10", "ASP.NET Core Minimal APIs", "Entity Framework Core", "PostgreSQL"]),
        new("FmStackMessaging", ["MQTT · Eclipse Mosquitto", "RabbitMQ"]),
        new("FmStackFrontend", ["Blazor Web App", "MudBlazor", ".NET MAUI Blazor Hybrid"]),
        new("FmStackOperations", [".NET Aspire", "Docker", "OpenTelemetry", "xUnit"]),
        new("FmStackEdge", ["Arduino", "USB serial → MQTT adapter"]),
    ];
}
