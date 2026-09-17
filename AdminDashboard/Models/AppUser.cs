namespace AdminDashboard.Models;

public enum UserRole
{
    Admin,
    Editor,
    Viewer
}

public enum UserStatus
{
    Active,
    Invited,
    Suspended
}

public class AppUser
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public DateTime JoinedOn { get; set; }
    public string Avatar => Name.Length > 0
        ? string.Concat(Name.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Take(2)
            .Select(p => char.ToUpperInvariant(p[0])))
        : "?";
}
