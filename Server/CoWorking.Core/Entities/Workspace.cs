namespace CoWorking.Core.Entities;

public class Workspace
{
    public int Id { get; set; }
    public WorkspaceType Type { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public List<WorkspaceIcon> WorkspaceIcons { get; set; } = new();
    public List<Picture> Pictures { get; set; } = new();
    public List<Room> Rooms { get; set; } = new();
}
