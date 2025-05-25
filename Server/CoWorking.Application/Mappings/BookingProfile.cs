using AutoMapper;
using CoWorking.Application.DTOs.Booking;
using CoWorking.Core.Entities;

namespace CoWorking.Application.Mappings;

public class BookingProfile : Profile
{
	public BookingProfile()
	{
        CreateMap<CreateBookingDTO, Booking>();

        CreateMap<Booking, BookingDTO>()
			.ForMember(dest => dest.WorkspaceName,opt => 
				opt.MapFrom(src => src.Room.Workspace.Name))
			.ForMember(dest => dest.RoomCapacity, opt =>
				opt.MapFrom(src => src.Room.Capacity))
			.ForMember(dest => dest.Duration, opt =>
				opt.MapFrom(src => src.EndDateTime - src.StartDateTime));

		CreateMap<Booking, PatchBookingDTO>()
			.ForMember(dest => dest.WorkspaceType, opt =>
                opt.MapFrom(src => src.Room.Workspace.Type))
			.ForMember(dest => dest.WorkspaceName, opt =>
			opt.MapFrom(src => src.Room.Workspace.Name))
			.ForMember(dest => dest.SelectedRoomId, opt =>
				opt.MapFrom(src => src.RoomId))
			.ForMember(dest => dest.Rooms, opt =>
				opt.MapFrom(src => src.Room.Workspace.Rooms));

		CreateMap<PatchBookingDTO, Booking>()
			.ForMember(dest => dest.RoomId, opt =>
			opt.MapFrom(src => src.SelectedRoomId));
	}
}
