using CoWorking.Core.Enums;

namespace CoWorking.Core.Entities;

public class Booking
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; } = default!;
}
