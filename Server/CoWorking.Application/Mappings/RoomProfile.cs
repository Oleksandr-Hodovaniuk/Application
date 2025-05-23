using AutoMapper;
using CoWorking.Application.DTOs;
using CoWorking.Core.Entities;

namespace CoWorking.Application.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomDTO>();
    }
}
