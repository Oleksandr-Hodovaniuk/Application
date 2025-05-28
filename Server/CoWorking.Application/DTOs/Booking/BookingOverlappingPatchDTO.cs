namespace CoWorking.Application.DTOs.Booking;

public class BookingOverlappingPatchDTO
{
    public int BookingId { get; set; }
    public string Email { get; set; } = default!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
