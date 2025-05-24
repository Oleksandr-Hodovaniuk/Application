namespace CoWorking.Application.DTOs;

public class BookingPatchDTO
{
    public string? Name { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public int? RoomId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
