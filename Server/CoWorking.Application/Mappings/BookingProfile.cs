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
			.ForMember(dest => dest.WorkspacePicture, opt =>
				opt.MapFrom(src => src.Room.Workspace.Pictures.FirstOrDefault() != null
				? src.Room.Workspace.Pictures.FirstOrDefault()!.Name
				: null))
			.ForMember(dest => dest.WorkspaceName, opt =>
				opt.MapFrom(src => src.Room.Workspace.Name))
			.ForMember(dest => dest.RoomCapacity, opt =>
				opt.MapFrom(src => src.Room.Capacity));
		
		CreateMap<Booking, PatchBookingDTO>()
			.ForMember(dest => dest.SelectedWorkspaceId, opt =>
                opt.MapFrom(src => src.Room.Workspace.Id))
			.ForMember(dest => dest.SelectedRoomId, opt =>
				opt.MapFrom(src => src.RoomId));

		CreateMap<PatchBookingDTO, Booking>()
			.ForMember(dest => dest.RoomId, opt =>
			opt.MapFrom(src => src.SelectedRoomId));
	}
}
