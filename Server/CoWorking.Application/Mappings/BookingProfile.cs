using AutoMapper;
using CoWorking.Application.DTOs;
using CoWorking.Core.Entities;

namespace CoWorking.Application.Mappings;

public class BookingProfile : Profile
{
	public BookingProfile()
	{
        CreateMap<BookingCreateDTO, Booking>();

        CreateMap<Booking, BookingDTO>()
			.ForMember(dest => dest.WorkspaceType,opt => 
				opt.MapFrom(src => src.Room.Workspace.Name))
			.ForMember(dest => dest.RoomCapacity, opt =>
				opt.MapFrom(src => src.Room.Capacity))
			.ForMember(dest => dest.Duration, opt =>
				opt.MapFrom(src => src.EndTime - src.StartTime));

		CreateMap<Booking, BookingPatchDTO>()
			.ForMember(dest => dest.WorkspaceType, opt =>
                opt.MapFrom(src => src.Room.Workspace.Name))
			.ForMember(dest => dest.RoomCapacities, opt =>
				opt.MapFrom(src => src.Room.Workspace.Rooms.Select(r => r.Capacity).ToList()));

		CreateMap<BookingPatchDTO, Booking>();
	}
}
