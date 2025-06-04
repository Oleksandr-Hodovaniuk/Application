namespace CoWorking.Core.Entities;

public class Coworking
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Address Addresses { get; set; } = default!;
    public List<Workspace> Workspaces { get; set; } = new();
}
