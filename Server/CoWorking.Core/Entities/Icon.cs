namespace CoWorking.Core.Entities;

public class Icon
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;

    public List<WorkspaceIcon> WorkspaceIcons { get; set; } = default!;
}
