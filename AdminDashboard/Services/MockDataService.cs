using AdminDashboard.Models;

namespace AdminDashboard.Services;

/// <summary>
/// In-memory stand-in for a real data layer (e.g. EF Core + a database).
/// Swap this out for a real repository/DbContext when wiring up persistence.
/// </summary>
public class MockDataService
{
    private readonly List<AppUser> _users;
    private readonly List<ProjectItem> _projects;

    public MockDataService()
    {
        _users = GenerateUsers();
        _projects = GenerateProjects();
    }

    public IReadOnlyList<AppUser> GetUsers() => _users;

    public IReadOnlyList<ProjectItem> GetProjects() => _projects;

    public int TotalUsers => _users.Count;

    public int ActiveProjects => _projects.Count(p => p.Status == ProjectStatus.Active);

    public decimal MonthlyRevenue => 48250m;

    public double GrowthPercent => 12.4;

    public bool AddUser(AppUser user)
    {
        user.Id = _users.Count == 0 ? 1 : _users.Max(u => u.Id) + 1;
        _users.Add(user);
        return true;
    }

    public bool DeleteUser(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user is null) return false;
        _users.Remove(user);
        return true;
    }

    public List<ChartPoint> GetRevenueOverTime()
    {
        string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };
        double[] values = { 18500, 21200, 19800, 24300, 27600, 31200, 35400, 41800, 48250 };
        return months.Select((m, i) => new ChartPoint { Label = m, Value = values[i] }).ToList();
    }

    public List<ChartPoint> GetSignupsByMonth()
    {
        string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };
        double[] values = { 42, 55, 48, 63, 71, 68, 84, 92, 101 };
        return months.Select((m, i) => new ChartPoint { Label = m, Value = values[i] }).ToList();
    }

    private static List<AppUser> GenerateUsers()
    {
        var firstNames = new[] { "Ava", "Liam", "Noah", "Emma", "Olivia", "Ethan", "Sophia", "Mason", "Isabella", "Lucas", "Mia", "James", "Amelia", "Benjamin", "Harper", "Zanele", "Sipho", "Naledi", "Thabo", "Lindiwe" };
        var lastNames = new[] { "Carter", "Bennett", "Reyes", "Coleman", "Foster", "Hayes", "Brooks", "Powell", "Simmons", "Ward", "Nkosi", "Dlamini", "Mokoena", "Khumalo", "Mahlangu" };
        var domains = new[] { "acme.co", "brightlabs.io", "northwind.dev", "vertex-inc.com" };

        var random = new Random(42);
        var users = new List<AppUser>();

        for (int i = 1; i <= 47; i++)
        {
            var first = firstNames[random.Next(firstNames.Length)];
            var last = lastNames[random.Next(lastNames.Length)];
            var role = (UserRole)random.Next(0, 3);
            var status = (UserStatus)random.Next(0, 3);

            users.Add(new AppUser
            {
                Id = i,
                Name = $"{first} {last}",
                Email = $"{first.ToLower()}.{last.ToLower()}@{domains[random.Next(domains.Length)]}",
                Role = role,
                Status = status,
                JoinedOn = DateTime.Today.AddDays(-random.Next(10, 720))
            });
        }

        // Make sure there's always at least one predictable admin for demos.
        users[0] = new AppUser
        {
            Id = users[0].Id,
            Name = "Jordan Admin",
            Email = "jordan.admin@acme.co",
            Role = UserRole.Admin,
            Status = UserStatus.Active,
            JoinedOn = DateTime.Today.AddDays(-500)
        };

        return users;
    }

    private static List<ProjectItem> GenerateProjects()
    {
        var names = new[]
        {
            "Website Relaunch", "Mobile App v2", "Q3 Marketing Push", "API Migration",
            "Customer Portal", "Internal Tooling", "Design System", "Data Warehouse",
            "Onboarding Revamp", "Payments Integration"
        };
        var owners = new[] { "Ava Carter", "Liam Bennett", "Noah Reyes", "Sophia Brooks", "Thabo Mokoena" };
        var random = new Random(7);

        return names.Select((n, i) => new ProjectItem
        {
            Id = i + 1,
            Name = n,
            Owner = owners[random.Next(owners.Length)],
            Status = (ProjectStatus)random.Next(0, 4),
            ProgressPercent = random.Next(5, 100),
            DueDate = DateTime.Today.AddDays(random.Next(-10, 90))
        }).ToList();
    }
}
