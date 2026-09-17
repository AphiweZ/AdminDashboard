using AdminDashboard.Models;

namespace AdminDashboard.Services;

/// <summary>
/// Stands in for a real authentication system (e.g. ASP.NET Core Identity,
/// Entra ID, Auth0). In a real app, the role would come from the user's
/// claims/session after sign-in. Here it's a switchable value so you can
/// see role-based access control (Step 4 of the challenge) in action
/// without wiring up a full auth pipeline.
/// </summary>
public class CurrentUserService
{
    public string Name { get; private set; } = "Jordan Admin";
    public UserRole Role { get; private set; } = UserRole.Admin;

    public event Action? OnChange;

    public void SetRole(UserRole role)
    {
        if (Role == role) return;
        Role = role;
        Name = role switch
        {
            UserRole.Admin => "Jordan Admin",
            UserRole.Editor => "Sam Editor",
            UserRole.Viewer => "Riley Viewer",
            _ => Name
        };
        OnChange?.Invoke();
    }

    public bool CanManageUsers => Role == UserRole.Admin;
    public bool CanEditSettings => Role is UserRole.Admin or UserRole.Editor;
}
