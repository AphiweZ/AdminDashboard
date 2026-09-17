namespace AdminDashboard.Models;

public enum ProjectStatus
{
    Planning,
    Active,
    OnHold,
    Completed
}

public class ProjectItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public int ProgressPercent { get; set; }
    public DateTime DueDate { get; set; }
}
