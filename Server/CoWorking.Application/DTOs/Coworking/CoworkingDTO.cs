namespace CoWorking.Application.DTOs.Coworking;

public class CoworkingDTO
{
    public int Id { get; set; }
    public string CoworkingPicture { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Address { get; set; } = default!;
    public int Desks { get; set; }
    public int PrivateRooms { get; set; }
    public int MeetingRooms { get; set; }
}
