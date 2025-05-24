using CoWorking.Core.Enums;

namespace CoWorking.Application.DTOs;

public class BookingDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string WorkspaceName { get; set; } = default!;
    public int RoomCapacity { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
}
