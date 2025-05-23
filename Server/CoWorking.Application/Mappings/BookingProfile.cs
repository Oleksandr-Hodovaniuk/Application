using AutoMapper;
using CoWorking.Application.DTOs;
using CoWorking.Core.Entities;

namespace CoWorking.Application.Mappings;

public class BookingProfile : Profile
{
	public BookingProfile()
	{
		CreateMap<Booking, BookingDTO>()
			.ForMember(dest => dest.WorkspaceName,opt => 
				opt.MapFrom(src => src.Room.Workspace.Name))
			.ForMember(dest => dest.Duration, opt =>
				opt.MapFrom(src => src.EndTime - src.StartTime));
	}
}
