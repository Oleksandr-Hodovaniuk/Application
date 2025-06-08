using CoWorking.Application.DTOs.AiAssistant;

namespace CoWorking.Application.Interfaces.Services;

public interface IAiAssistantService
{
    Task<AiAssistantResponseDTO> AskAsync(string question, IEnumerable<AiBookingDTO> bookings, CancellationToken cancellationToken);
}
