namespace CoWorking.Application.DTOs.Room;

public class RoomAvailabePatchDTO
{
    public int BookingId { get; set; }
    public int RoomId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
