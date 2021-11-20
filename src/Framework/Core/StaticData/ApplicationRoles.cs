namespace Core.StaticData;

public class ApplicationRoles
{
    public const string TenantAdmin = "TenantAdmin";

    public const string TeamAdmin = "TeamAdmin";

    public const string TeamMember = "TeamMember";

    // For Role property of Authorize attribute in actions and controllers
    public const string TeamAdmin_ToTheTop = "TenantAdmin,TeamAdmin";

    public const string TeamMember_ToTheTop = "TenantAdmin,TeamAdmin,TeamMember";
}
