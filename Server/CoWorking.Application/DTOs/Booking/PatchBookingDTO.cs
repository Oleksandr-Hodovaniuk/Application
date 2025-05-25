using CoWorking.Application.DTOs.Room;

namespace CoWorking.Application.DTOs.Booking;

public class PatchBookingDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? WorkspaceType { get; set; }
    public string? WorkspaceName { get; set; }
    public List<RoomCharacteristicsDTO>? Rooms { get; set; }
    public int? SelectedRoomId { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
}
