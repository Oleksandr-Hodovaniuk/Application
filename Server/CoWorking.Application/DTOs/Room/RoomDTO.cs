using CoWorking.Application.DTOs.Booking;

namespace CoWorking.Application.DTOs.Room;

public class RoomDTO
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public int Quantity { get; set; }
    public int Availability { get; set; } 
}
