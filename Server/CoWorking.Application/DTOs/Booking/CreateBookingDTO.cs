﻿using CoWorking.Application.DTOs.Room;
using CoWorking.Application.DTOs.Workspace;

namespace CoWorking.Application.DTOs.Booking;

public class CreateBookingDTO
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int RoomId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
