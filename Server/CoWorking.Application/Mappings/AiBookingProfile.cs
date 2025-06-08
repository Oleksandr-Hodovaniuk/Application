using AutoMapper;
using CoWorking.Application.DTOs.AiAssistant;
using CoWorking.Core.Entities;

namespace CoWorking.Application.Mappings;

public class AiBookingProfile : Profile
{
    public AiBookingProfile()
    {
        CreateMap<Booking, AiBookingDTO>()
            .ForMember(dest => dest.Coworking, opt =>
                opt.MapFrom(src => src.Room.Workspace.Coworking.Name))
            .ForMember(dest => dest.Workspace, opt =>
                opt.MapFrom(src => src.Room.Workspace.Name))
            .ForMember(dest => dest.RoomCapacity, opt =>
                opt.MapFrom(src => src.Room.Capacity));
    }
}
