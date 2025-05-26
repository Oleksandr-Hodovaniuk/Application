using CoWorking.Application.DTOs.Room;
using CoWorking.Application.DTOs.Workspace;

namespace CoWorking.Application.DTOs.Booking;

public class PatchBookingDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public List<DropDownWorkspaceDTO>? Workspaces { get; set; }
    public int? SelectedWorkspaceId { get; set; }
    public int? SelectedRoomId { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
}
