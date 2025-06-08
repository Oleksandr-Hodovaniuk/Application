namespace CoWorking.Application.Models;

public class AiAssistantSettings
{
    public string ApiKey { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string Endpoint { get; set; } = default!;
}
