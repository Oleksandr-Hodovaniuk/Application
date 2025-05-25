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
			.ForMember(dest => dest.WorkspaceName,opt => 
				opt.MapFrom(src => src.Room.Workspace.Name))
			.ForMember(dest => dest.RoomCapacity, opt =>
				opt.MapFrom(src => src.Room.Capacity))
			.ForMember(dest => dest.Duration, opt =>
				opt.MapFrom(src => src.EndDateTime - src.StartDateTime));

		CreateMap<Booking, BookingPatchDTO>()
			.ForMember(dest => dest.WorkspaceType, opt =>
                opt.MapFrom(src => src.Room.Workspace.Type))
			.ForMember(dest => dest.WorkspaceName, opt =>
			opt.MapFrom(src => src.Room.Workspace.Name))
			.ForMember(dest => dest.SelectedRoomId, opt =>
				opt.MapFrom(src => src.RoomId))
			.ForMember(dest => dest.Rooms, opt =>
				opt.MapFrom(src => src.Room.Workspace.Rooms));

		CreateMap<BookingPatchDTO, Booking>();
	}
}
