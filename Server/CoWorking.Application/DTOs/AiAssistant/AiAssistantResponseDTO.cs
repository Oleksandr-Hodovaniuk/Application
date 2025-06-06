namespace CoWorking.Application.DTOs.AiAssistant;

public class AiAssistantResponseDTO
{
    public string Message { get; set; } = default!;
    public List<AiBookingDTO> Bookings { get; set; } = new();
}
