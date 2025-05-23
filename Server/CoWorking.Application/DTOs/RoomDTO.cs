namespace CoWorking.Application.DTOs;

public class RoomDTO
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public int Quantity { get; set; }
    public List<BookingDTO> Bookings { get; set; } = default!;
}
