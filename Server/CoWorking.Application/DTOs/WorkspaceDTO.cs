namespace CoWorking.Application.DTOs;

public class WorkspaceDTO
{
    public int Id { get; set; }
    public string Type { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<string>? Icons { get; set; } 
    public List<string>? Pictures { get; set; }
    public List<RoomDTO>? Rooms { get; set; }
}
