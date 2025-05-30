﻿namespace CoWorking.Application.DTOs.Booking;

public class BookingDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string WorkspacePicture { get; set; } = default!;
    public string WorkspaceName { get; set; } = default!;
    public int RoomCapacity { get; set; } = default!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
