﻿namespace CoWorking.Core.Entities;

public class Room
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public int Quantity { get; set; }

    public int WorkspaceId { get; set; }
    public Workspace Workspace { get; set; } = default!;
    public List<Booking> Bookings { get; set; } = new();
}
