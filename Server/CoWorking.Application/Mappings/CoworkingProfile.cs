using AutoMapper;
using CoWorking.Application.DTOs.Coworking;
using CoWorking.Core.Entities;
using CoWorking.Core.Enums;

namespace CoWorking.Application.Mappings;

public class CoworkingProfile : Profile
{
    public CoworkingProfile()
    {
        CreateMap<Coworking, CoworkingDTO>()
            .ForMember(dest => dest.Address, opt =>
                opt.MapFrom(src => $"{src.Addresses.BuildingNumber} {src.Addresses.Street}, {src.Addresses.City}"))
            .ForMember(dest => dest.Desks, opt =>
                opt.MapFrom(src =>
                    src.Workspaces.Where(w => w.Type == WorkspaceType.OpenSpace)
                        .SelectMany(w => w.Rooms)
                        .Sum(r => r.Quantity)
                )
            )
            .ForMember(dest => dest.PrivateRooms, opt =>
                opt.MapFrom(src => src.Workspaces.Where(w => w.Type == WorkspaceType.PrivateRoom)
                    .SelectMany(w => w.Rooms)
                    .Sum(r => r.Quantity)
                )
            )
            .ForMember(dest => dest.MeetingRooms, opt =>
                opt.MapFrom(src => src.Workspaces.Where(w => w.Type == WorkspaceType.MeetingRoom)
                    .SelectMany(w => w.Rooms)
                    .Sum(r => r.Quantity)
                )
            );
            
    }
}
