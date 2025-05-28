namespace CoWorking.Application.DTOs.Booking;

public class BookingOverlappingDTO
{
    public string Email { get; set; } = default!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
