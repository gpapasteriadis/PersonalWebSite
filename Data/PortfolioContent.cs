using MudBlazor;
using PersonalWebSite.Models;

namespace PersonalWebSite.Data;

/// <summary>
/// The site's content catalog. Sections render these lists instead of hard-coding markup per item.
/// Texts live in the resx files and links in wwwroot/appsettings.json; this file only wires them together.
/// </summary>
public static class PortfolioContent
{
    public static IReadOnlyList<NavItem> Navigation { get; } =
    [
        new("jobs", "Experience"),
        new("projects", "Projects"),
        new("skills", "Skills"),
        new("contact", "Contact"),
    ];

    /// <summary>Link back to the Projects section from other pages.</summary>
    public const string ProjectsHref = "/#projects";

    /// <summary>Newest first.</summary>
    public static IReadOnlyList<Job> Jobs { get; } =
    [
        new("Indeavor", "Indeavor", "SoftwareEngineer", "IndeavorDate", "IndeavorDesc", "JobLinks:Indeavor"),
        new("PwC Greece", "PwC Greece", "SoftwareEngineer", "PwCDate", "PwCDesc", "JobLinks:PwC"),
        new("ArmyDepartment", "Army", "SoftwareEngineer", "ArmyDate", "ArmyDesc", "JobLinks:Army", IsLocalized: true),
        new("Terracom S.A.", "Terracom S.A.", "SoftwareEngineer", "TerracomDate", "TerracomDesc", "JobLinks:Terracom"),
        new("CoTheta", "CoTheta", "SoftwareEngineerIntern", "CothetaDate", "CothetaDesc", "JobLinks:CoTheta"),
    ];

    public static Project PersonalWebsite { get; } = new(
        "PersonalWebsite", "PersonalWebsiteTitle", "PersonalWebsiteDesc",
        [".NET 10", "Blazor WebAssembly", "MudBlazor"]);

    public static Project MobileApp { get; } = new(
        "MobileApp", "MobileAppTitle1", "MobileAppDesc",
        ["Ionic Framework", "Angular", "NestJS", "Apollo GraphQL"]);

    /// <summary>Has its own case-study page; its content lives in <see cref="FieldmaticsContent"/>.</summary>
    public static Project Fieldmatics { get; } = new(
        "FmLabel", "FmTitle", "FmSummary",
        [".NET 10", "ASP.NET Core", "Blazor", "MudBlazor", "Entity Framework Core", "PostgreSQL",
         "MQTT", "RabbitMQ", ".NET Aspire", "Docker", "xUnit"]);

    public static IReadOnlyList<Skill> Skills { get; } =
    [
        new(".NET Core", SkillIcons.DotNetCore, ".NetCoreDesc", "SkillAcquired_JobProject", "jackInTheBox"),
        new("Blazor", SkillIcons.Blazor, "BlazorDesc", "SkillAcquired_JobProjectCourse", "tada"),
        new("Angular", SkillIcons.Angular, "AngularDesc", "SkillAcquired_Job", "bounceIn"),
        new("MongoDB", SkillIcons.MongoDB, "MongoDBDesc", "SkillAcquired_Project", "jello"),
        new("SQL", SkillIcons.Sql, "SQLDesc", "SkillAcquired_JobProject", "flipInY"),
        new("EF Core", SkillIcons.EfCore, "EFCoreDesc", "SkillAcquired_JobCourse", "flipInX"),
        new("Docker", SkillIcons.Docker, "DockerDesc", "SkillAcquired_JobCourse", "rubberBand"),
        new("SignalR", SkillIcons.SignalR, "SignalRDesc", "SkillAcquired_Job", "tada"),
        new("RabbitMQ", SkillIcons.RabbitMQ, "RabbitMQDesc", "SkillAcquired_Job", "bounceIn"),
    ];

    public static IReadOnlyList<SocialLink> SocialLinks { get; } =
    [
        new("LinkedIn", Icons.Custom.Brands.LinkedIn, "SocialNetworkLinks:LinkedIn"),
        new("GitHub", Icons.Custom.Brands.GitHub, "SocialNetworkLinks:GitHub"),
        new("Instagram", Icons.Custom.Brands.Instagram, "SocialNetworkLinks:Instagram"),
        new("Facebook", Icons.Custom.Brands.Facebook, "SocialNetworkLinks:Facebook"),
    ];

    public const string CvPath = "George-CV.pdf";
    public const string AvatarPath = "/Icons/me-removebg.png";
    public const string LogoPath = "/Icons/codeIcon-removebg.png";
}
