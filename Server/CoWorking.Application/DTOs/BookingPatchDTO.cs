namespace CoWorking.Application.DTOs;

public class BookingPatchDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? WorkspaceType { get; set; }
    public string? WorkspaceName { get; set; }
    public List<RoomDTO>? Rooms { get; set; }
    public int? SelectedRoomId { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
}
