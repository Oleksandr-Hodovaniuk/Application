namespace CoWorking.Core.Entities;

public class WorkspaceIcon
{
    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; } = default!;

    public int IconId { get; set; }
    public Icon Icon { get; set; } = default!;
}
