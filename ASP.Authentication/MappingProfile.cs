using ASP.Authentication.DTOs;
using ASP.Authentication.Entities;
using AutoMapper;

namespace ASP.Authentication;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserForRegisterationDto, User>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.Email));
    }
}
