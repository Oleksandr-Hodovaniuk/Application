namespace CoWorking.Core.Entities;

public class Booking
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; } = default!;
}
