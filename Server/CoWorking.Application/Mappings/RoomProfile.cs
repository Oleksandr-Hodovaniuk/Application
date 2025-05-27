using AutoMapper;
using CoWorking.Application.DTOs.Room;
using CoWorking.Core.Entities;

namespace CoWorking.Application.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomDTO>()
            .ForMember(dest => dest.Bookings, opt =>
                opt.MapFrom(src => src.Bookings));

        CreateMap<Room, RoomCharacteristicsDTO>();
    }
}
