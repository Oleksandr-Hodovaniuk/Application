using AutoMapper;
using CoWorking.Application.DTOs.Room;
using CoWorking.Core.Entities;

namespace CoWorking.Application.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomDTO>()          
            .ForMember(dest => dest.Availability, opt =>
            opt.MapFrom(src => src.Quantity - src.Bookings
                .Where(b => b.StartDateTime <= DateTime.Now
                && b.EndDateTime > DateTime.Now).Count()));

        CreateMap<Room, RoomCharacteristicsDTO>();
    }
}
