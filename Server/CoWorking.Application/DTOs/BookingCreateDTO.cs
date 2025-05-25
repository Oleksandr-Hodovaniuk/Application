namespace CoWorking.Application.DTOs;

public class BookingCreateDTO
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int RoomId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
