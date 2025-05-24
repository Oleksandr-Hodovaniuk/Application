namespace CoWorking.Application.DTOs;

public class BookingPatchDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? WorkspaceType { get; set; }
    public List<int>? RoomCapacities { get; set; }
    public int? RoomId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
