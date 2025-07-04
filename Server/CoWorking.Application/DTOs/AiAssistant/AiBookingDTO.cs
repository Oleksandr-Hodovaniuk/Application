﻿namespace CoWorking.Application.DTOs.AiAssistant;

public class AiBookingDTO
{
    public string Coworking { get; set; } = default!;
    public string Workspace { get; set; } = default!;
    public int RoomCapacity { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
