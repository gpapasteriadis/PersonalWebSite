using MudBlazor;
using PersonalWebSite.Models;

namespace PersonalWebSite.Data;

/// <summary>
/// Content of the /Fieldmatics case-study page. The page is business-first: the problem, what the SaaS brings,
/// how a grower uses it, and two simple diagrams. Fieldmatics is a private, proprietary repository, so keep
/// technical detail light and never show code, configuration, topic names, internal decision IDs or repo links.
/// Mark roadmap features as planned. Source material: the Fieldmatics README, ARCHITECTURE.md and docs/architecture/.
/// </summary>
public static class FieldmaticsContent
{
    public const string PagePath = "/Fieldmatics";

    /// <summary>In-page navigation; section ids double as scrollspy targets.</summary>
    public static IReadOnlyList<NavItem> Sections { get; } =
    [
        new("fm-problem", "FmProblemTitle", PagePath),
        new("fm-value", "FmValueTitle", PagePath),
        new("fm-how", "FmHowTitle", PagePath),
        new("fm-picture", "FmPictureTitle", PagePath),
        new("fm-trust", "FmTrustTitle", PagePath),
    ];

    public static IReadOnlyList<string> EnvironmentKeys { get; } =
        ["FmEnvGreenhouses", "FmEnvGardens", "FmEnvFields", "FmEnvOliveGroves"];

    /// <summary>What the SaaS brings to growers.</summary>
    public static IReadOnlyList<Feature> Value { get; } =
    [
        new(Icons.Material.Filled.Dashboard, "FmVal1Title", "FmVal1Text", FeatureStatus.InProgress),
        new(Icons.Material.Filled.Timeline, "FmVal2Title", "FmVal2Text"),
        new(Icons.Material.Filled.Hub, "FmVal3Title", "FmVal3Text"),
        new(Icons.Material.Filled.NotificationsActive, "FmVal4Title", "FmVal4Text", FeatureStatus.Planned),
        new(Icons.Material.Filled.WaterDrop, "FmVal5Title", "FmVal5Text", FeatureStatus.Planned),
        new(Icons.Material.Filled.Cloud, "FmVal6Title", "FmVal6Text", FeatureStatus.Planned),
    ];

    /// <summary>A grower's path from setup to automation.</summary>
    public static IReadOnlyList<JourneyStep> Journey { get; } =
    [
        new(Icons.Material.Filled.AddLocationAlt, JourneyZone.Setup, "FmStep1Title", "FmStep1Text", FeatureStatus.InProgress),
        new(Icons.Material.Filled.Sensors, JourneyZone.Setup, "FmStep2Title", "FmStep2Text"),
        new(Icons.Material.Filled.CloudSync, JourneyZone.Daily, "FmStep3Title", "FmStep3Text"),
        new(Icons.Material.Filled.Insights, JourneyZone.Daily, "FmStep4Title", "FmStep4Text", FeatureStatus.InProgress),
        new(Icons.Material.Filled.AutoMode, JourneyZone.Next, "FmStep5Title", "FmStep5Text"),
    ];

    public static string ZoneKey(JourneyZone zone) => zone switch
    {
        JourneyZone.Setup => "FmZoneSetup",
        JourneyZone.Daily => "FmZoneDaily",
        JourneyZone.Next => "FmZoneNext",
        _ => throw new ArgumentOutOfRangeException(nameof(zone), zone, null),
    };

    /// <summary>Badge label for a feature or journey step; null when it is available today.</summary>
    public static string? StatusKey(FeatureStatus status) => status switch
    {
        FeatureStatus.InProgress => "FmInProgress",
        FeatureStatus.Planned => "FmPlanned",
        _ => null,
    };

    /// <summary>Deliberately non-technical: no product, protocol or process names.</summary>
    public static IReadOnlyList<Diagram> Diagrams { get; } =
    [
        new("fm-diagram-flow", "FmDiagFlowTitle", "FmDiagFlowText", "FmDiagFlowSource"),
        new("fm-diagram-tenants", "FmDiagTenantsTitle", "FmDiagTenantsText", "FmDiagTenantsSource"),
    ];

    /// <summary>Engineering principles, phrased as what they mean for the customer.</summary>
    public static IReadOnlyList<Feature> Trust { get; } =
    [
        new(Icons.Material.Filled.TaskAlt, "FmTrust1Title", "FmTrust1Text"),
        new(Icons.Material.Filled.VerifiedUser, "FmTrust2Title", "FmTrust2Text"),
        new(Icons.Material.Filled.History, "FmTrust3Title", "FmTrust3Text"),
        new(Icons.Material.Filled.TrendingUp, "FmTrust4Title", "FmTrust4Text"),
    ];
}
