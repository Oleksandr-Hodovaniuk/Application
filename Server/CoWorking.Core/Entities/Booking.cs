namespace CoWorking.Core.Entities;

public class Booking
{
    public int Id { get; set; }
    public Guid GroupBookingId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public int RoomConfigurationId { get; set; }
    public Space SpaceConfiguration { get; set; } = default!;
}
