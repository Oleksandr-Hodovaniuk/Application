namespace CoWorking.Application.DTOs.Room;

public class RoomAvailableCreateDTO
{
    public int RoomId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
