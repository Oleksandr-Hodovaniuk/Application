using AutoMapper;
using CoWorking.Application.DTOs.Workspace;
using CoWorking.Core.Entities;

namespace CoWorking.Application.Mappings;

public class WorkspaceProfile : Profile
{
    public WorkspaceProfile()
    {
        CreateMap<Workspace, WorkspaceDTO>()
            .ForMember(dest => dest.Type, opt =>
                opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Icons,opt =>
                opt.MapFrom(src => src.WorkspaceIcons.Select(wi => wi.Icon.Name)))
            .ForMember(dest => dest.Pictures,opt =>
                opt.MapFrom(src => src.Pictures.Select(p => p.Name)))
            .ForMember(dest => dest.Rooms,opt =>
                opt.MapFrom(src => src.Rooms));

        CreateMap<Workspace, DropDownWorkspaceDTO>();
    }
}
