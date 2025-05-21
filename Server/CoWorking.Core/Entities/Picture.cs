namespace CoWorking.Core.Entities;

public class Picture
{
    public int Id { get; set; }
    public string Url { get; set; } = default!;

    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; } = default!;
}
