using CoWorking.Application.DTOs.Room;

namespace CoWorking.Application.DTOs.Booking;

public class CreateBookingDTO
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string WorkspaceType { get; set; } = default!;
    public string WorkspaceName { get; set; } = default!;
    public List<RoomCharacteristicsDTO>? Rooms { get; set; }
    public int RoomId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
