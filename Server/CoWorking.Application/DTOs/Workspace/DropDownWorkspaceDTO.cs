using CoWorking.Application.DTOs.Room;

namespace CoWorking.Application.DTOs.Workspace;

public class DropDownWorkspaceDTO
{
    public int Id { get; set; }
    public string Type { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int MaxBookingDuration { get; set; }
    public List<RoomCharacteristicsDTO> Rooms { get; set; } = new();
}
